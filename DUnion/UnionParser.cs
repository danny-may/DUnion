using DUnion.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using static DUnion.Models.Constants;
using Location = DUnion.Models.Location;

namespace DUnion;

internal static class UnionParser
{
    public static Union? Parse(ISymbol symbol, SemanticModel model, AttributeData[] attributes, GeneratorContext context)
    {
        if (symbol is not INamedTypeSymbol { TypeKind: TypeKind.Class or TypeKind.Struct } caseHolder)
        {
            context.AttributeOnlyOnClassOrStruct(DUnionAttribute, attributes);
            return null;
        }

        var cases = caseHolder.GetTypeMembers()
            .Select(t => ParseCase(t, context))
            .Where(t => t is not null)!
            .Select(t => t!.Value)
            .ToArray();

        if (cases.Length < 2)
            context.UnionsShouldHave2OrMoreCases(attributes);

        var id = ParseTypeId(caseHolder, s => s.Name);
        id = id with
        {
            TypeParameters = new(id.TypeParameters.Select(p => (id, p))
                .Concat(cases.Select(c => c.Case).SelectMany(c => c.Id.TypeParameters, (c, p) => (c.Id, p)))
                .GroupBy(p => p.p.Name)
                .Select(g =>
                {
                    var parameters = g.ToArray();
                    var first = parameters[0];
                    var constraints = first.p.Constraints.ToImmutableHashSet();

                    foreach (var parameter in parameters.Skip(1))
                    {
                        if (constraints.SymmetricExcept(parameter.p.Constraints).Count > 0)
                            context.IncompatibleTypeParameters(parameter.Item1, parameter.p, first.Item1, first.p);
                    }

                    return first.p;
                }))
        };

        var definition = ParseUnionDefinition(caseHolder, id, model, context);
        var config = ParseUnionConfig(attributes, context);

        ValidateCaseAccessibility(definition, cases, context);

        return new(id, definition.TypeDefinition, config, new(cases.Select(c => c.Case)));
    }

    private static Func<ITypeParameterSymbol, string> CreateTypeParameterNameRemapping(INamedTypeSymbol symbol, GeneratorContext context)
    {
        var typeParameterNameRemapping = symbol.TypeParameters
            .Select(KeyValuePair<ISymbol, string> (p) =>
            {
                var attrs = p.GetAttributes()
                    .Where(attr => attr.AttributeClass?.ToDisplayString() == DUnionGenericAttribute)
                    .ToArray();
                if (attrs.Length == 0)
                    return new(p, p.Name);

                if (attrs.Length > 1)
                    context.DuplicateAttribute(DUnionGenericAttribute, attrs);

                var attr = attrs.FirstOrDefault(a => a.ConstructorArguments.Length > 0);
                if (attr is not { ConstructorArguments: [{ Value: string name }, ..] } || !SyntaxFacts.IsValidIdentifier(name))
                    context.AttributeArgumentMustBeAnIdentifier(attr, 0);
                else
                    return new(p, name);
                return new(p, p.Name);
            })
            .ToImmutableDictionary(SymbolEqualityComparer.Default);

        return v => typeParameterNameRemapping.TryGetValue(v, out var name) ? name : v.Name;
    }

    private static string? GetAttributeIdentifierProperty(IReadOnlyDictionary<string, AttributeProperty> properties, string propertyName, GeneratorContext context, out AttributeData? attribute)
    {
        if (GetAttributeProperty<string>(properties, propertyName, context, out attribute) is not string name)
            return null;

        if (!SyntaxFacts.IsValidIdentifier(name))
        {
            context.AttributePropertyMustBeAnIdentifier(attribute!, propertyName);
            return null;
        }

        return name;
    }

    private static T? GetAttributeProperty<T>(IReadOnlyDictionary<string, AttributeProperty> properties, string propertyName, GeneratorContext context, out AttributeData? attribute)
    {
        if (!properties.TryGetValue(propertyName, out var value))
        {
            attribute = null;
            return default;
        }

        attribute = value.Source;

        if (value is { Const.IsNull: true })
            return default;

        if (value is not { Const.Kind: TypedConstantKind.Primitive, Const.Value: T result })
        {
            context.AttributePropertyMustBeSpecificType(attribute!, propertyName, typeof(T));
            return default;
        }

        return result;
    }

    private static Sequence<Location> GetLocations(ISymbol symbol)
    {
        return new(symbol.Locations.Select(Location (l) => l));
    }

    private static IReadOnlyDictionary<string, AttributeProperty> GetProperties(IEnumerable<AttributeData> attributes)
    {
        return attributes
            .SelectMany(a => a.NamedArguments, (attr, arg) => (arg.Key, new AttributeProperty(arg.Value, attr)))
            .GroupBy(a => a.Key)
            .ToImmutableDictionary(g => g.Key, g => g.First().Item2);
    }

    private static bool IsAutoProperty(IPropertySymbol symbol, CancellationToken token)
    {
        return symbol.DeclaringSyntaxReferences
            .Select(r => r.GetSyntax(token))
            .OfType<PropertyDeclarationSyntax>()
            .SelectMany(s => s.AccessorList?.Accessors ?? [])
            .Any(a => a is { Body: null, ExpressionBody: null });
    }

    private static bool IsCaseExcluded(ILookup<string?, AttributeData> attributeNameLookup, GeneratorContext context)
    {
        var excludes = attributeNameLookup[DUnionCaseExcludeAttribute].ToArray();
        if (excludes.Length == 0)
            return false;

        if (excludes.Length > 1)
            context.DuplicateAttribute(DUnionCaseExcludeAttribute, excludes);

        var configs = attributeNameLookup[DUnionCaseAttribute].ToArray();
        if (configs.Length > 0)
            context.CaseIsConfiguredButAlsoExcluded(excludes, configs);

        return true;
    }

    private static UnionCaseWithSymbol? ParseCase(INamedTypeSymbol symbol, GeneratorContext context)
    {
        if (symbol is { IsRefLikeType: true, TypeKind: TypeKind.Struct })
            context.CaseCannotBeARefStruct(symbol);

        var attributeNameLookup = symbol.GetAttributes().ToLookup(attr => attr.AttributeClass?.ToDisplayString());
        if (IsCaseExcluded(attributeNameLookup, context))
            return null;

        var typeId = ParseTypeId(symbol, CreateTypeParameterNameRemapping(symbol, context));
        var configs = attributeNameLookup[DUnionCaseAttribute].ToArray();
        var config = ParseCaseConfig(typeId.Name, configs, context);
        var definition = new TypeDefinition(symbol.DeclaredAccessibility, symbol.TypeKind, symbol.IsRecord);

        return new(symbol, new(typeId, definition, config));
    }

    private static UnionCaseConfig ParseCaseConfig(string name, AttributeData[] attributes, GeneratorContext context)
    {
        var config = new UnionCaseConfig($"Is{name}", $"As{name}OrDefault");

        if (attributes.Length == 0)
            return config;

        if (attributes.Length > 1)
            context.DuplicateAttribute(DUnionCaseAttribute, attributes);

        var attribute = attributes.FirstOrDefault();
        if (GetAttributeIdentifierProperty(GetProperties(attributes), IsCaseName, context, out _) is string isCaseName)
            config = config with { IsCaseName = isCaseName };
        if (GetAttributeIdentifierProperty(GetProperties(attributes), CaseOrDefaultName, context, out _) is string caseOrDefaultName)
            config = config with { CaseOrDefaultName = caseOrDefaultName };
        return config;
    }

    private static TypeId ParseTypeId(INamedTypeSymbol symbol, Func<ITypeParameterSymbol, string> getSymbolName)
    {
        var ns = symbol.GetNamespace();
        var containers = new Stack<TypeContainer>();
        var name = symbol.Name;

        for (var container = symbol.ContainingType; container is not null; container = container.ContainingType)
        {
            containers.Push(new TypeContainer(
                container.Name,
                new(container.TypeParameters.Select(p => ParseTypeParameter(p, getSymbolName))),
                container.IsRecord,
                container.TypeKind));
        }

        var typeParameters = symbol.TypeParameters.Select(p => ParseTypeParameter(p, getSymbolName));

        return new TypeId(ns, new(containers), name, new(typeParameters), GetLocations(symbol));
    }

    private static TypeParameter ParseTypeParameter(ITypeParameterSymbol symbol, Func<ITypeParameterSymbol, string> getSymbolName)
    {
        var constraints = new List<string>();
        if (symbol.HasNotNullConstraint)
            constraints.Add("notnull");
        if (symbol.HasValueTypeConstraint)
            constraints.Add("struct");
        if (symbol.HasReferenceTypeConstraint)
            constraints.Add(symbol.ReferenceTypeConstraintNullableAnnotation == NullableAnnotation.Annotated ? "class?" : "class");
        if (symbol.HasUnmanagedTypeConstraint)
            constraints.Add("unmanaged");
        foreach (var type in symbol.ConstraintTypes)
            constraints.Add(FullyQualifiedName(type));
        if (symbol.HasConstructorConstraint)
            constraints.Add("new()");

        return new(getSymbolName(symbol), new(constraints), GetLocations(symbol));

        string FullyQualifiedName(ITypeSymbol type)
        {
            if (type is ITypeParameterSymbol parameter)
                return getSymbolName(parameter);

            var name = type is INamedTypeSymbol { TypeArguments.Length: > 0 } namedType
                ? $"{type.Name}<{string.Join(", ", namedType.TypeArguments.Select(FullyQualifiedName))}>"
                : type.Name;

            if (type.ContainingType is not null)
                return $"{FullyQualifiedName(type.ContainingType)}.{name}";
            if (type.GetNamespace() is { Length: > 0 } ns)
                return $"{ns}.{name}";
            return name;
        }
    }

    private static UnionConfig ParseUnionConfig(AttributeData[] attributes, GeneratorContext context)
    {
        var result = new UnionConfig("_discriminator", "_value", "Switch", "Match", false);
        if (attributes.Length == 0)
            return result;

        if (attributes.Length > 1)
            context.DuplicateAttribute(DUnionAttribute, attributes);

        var properties = GetProperties(attributes);

        return new UnionConfig(
            GetAttributeIdentifierProperty(properties, DiscriminatorName, context, out _) ?? result.DiscriminatorName,
            GetAttributeIdentifierProperty(properties, ValueName, context, out _) ?? result.ValueName,
            GetAttributeIdentifierProperty(properties, SwitchName, context, out _) ?? result.SwitchName,
            GetAttributeIdentifierProperty(properties, MatchName, context, out _) ?? result.MatchName,
            GetAttributeProperty<bool?>(properties, UseUnsafe, context, out _) ?? result.UseUnsafe);
    }

    private static TypeDefinitionWithSymbol ParseUnionDefinition(
        INamedTypeSymbol caseHolder,
        TypeId id,
        SemanticModel model,
        GeneratorContext context)
    {
        if (model.Compilation.GetTypeByMetadataName(Helpers.ToFullName(id)) is { } existingUnion)
        {
            ReportIllegalBaseType(existingUnion, context);
            ReportStaticUnions(caseHolder, context, existingUnion);
            ReportIllegalConstructors(existingUnion, context);
            ReportInstanceFields(context, existingUnion);

            return new(existingUnion, new(existingUnion.DeclaredAccessibility, existingUnion.TypeKind, existingUnion.IsRecord));
        }

        if (id.TypeParameters.Length != caseHolder.TypeParameters.Length)
        {
            return new(caseHolder, new(caseHolder.DeclaredAccessibility, TypeKind.Struct, true));
        }

        return new(caseHolder, new(caseHolder.DeclaredAccessibility, caseHolder.TypeKind, caseHolder.IsRecord));
    }

    private static void ReportIllegalBaseType(INamedTypeSymbol union, GeneratorContext context)
    {
        if (union.BaseType is not { SpecialType: SpecialType.System_Object or SpecialType.System_ValueType })
            context.UnionCannotHaveABaseType(union);
    }

    private static void ReportIllegalConstructors(INamedTypeSymbol type, GeneratorContext context)
    {
        foreach (var ctor in type.Constructors
            .Where(ctor => !ctor.IsStatic)
            .Where(ctor => ctor.DeclaringSyntaxReferences.Length > 0)
            .Where(ctor => !ctor.DeclaringSyntaxReferences
                .Select(r => r.GetSyntax(context.CancellationToken))
                .OfType<ConstructorDeclarationSyntax>()
                .Any(ctor => ctor.Initializer?.ThisOrBaseKeyword.IsKind(SyntaxKind.ThisKeyword) ?? false)))
        {
            context.UnionConstructorMustCallAnotherConstructor(ctor);
        }
    }

    private static void ReportInstanceFields(GeneratorContext context, INamedTypeSymbol union)
    {
        var members = union
            .GetMembers()
            .Where(m => !m.IsStatic)
            .Where(m => m.DeclaringSyntaxReferences.Length > 0)
            .ToLookup(m => m.Kind);

        foreach (var field in members[SymbolKind.Field].OfType<IFieldSymbol>())
            context.UnionCannotHaveInstanceFields(field);

        foreach (var property in members[SymbolKind.Property].OfType<IPropertySymbol>().Where(x => IsAutoProperty(x, context.CancellationToken)))
            context.UnionCannotHaveInstanceAutoProperties(property);
    }

    private static void ReportStaticUnions(INamedTypeSymbol definition, GeneratorContext context, INamedTypeSymbol union)
    {
        if (union.IsStatic)
            context.UnionCannotBeStatic(definition);
    }

    private static void ValidateCaseAccessibility(TypeDefinitionWithSymbol union, UnionCaseWithSymbol[] cases, GeneratorContext context)
    {
        foreach (var (symbol, @case) in cases)
        {
            if (@case.Definition.Accessibility < union.TypeDefinition.Accessibility)
                context.CaseCannotBeLessAccessibleThanTheUnion(union.Symbol, symbol);
        }
    }

    private record class AttributeProperty(TypedConstant Const, AttributeData Source);
    private readonly record struct UnionCaseWithSymbol(INamedTypeSymbol Symbol, UnionCase Case);
    private readonly record struct TypeDefinitionWithSymbol(INamedTypeSymbol Symbol, TypeDefinition TypeDefinition);
}