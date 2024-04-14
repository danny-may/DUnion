using DUnion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static Microsoft.CodeAnalysis.CSharpExtensions;
using CA = Microsoft.CodeAnalysis;

namespace DUnion;

internal static class DiagnosticsExtensions
{
    private const string _id = "DYNUNION";

    private static readonly CA.DiagnosticDescriptor _attributeArgumentMustBeAnIdentifier = new(
        id: $"{_id}008",
        title: "Invalid argument value.",
        messageFormat: "The {0} constructor argument value for {1} must be a legal identifier.",
        category: "SourceGenerator",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _attributeOnlyOnCase = new(
        id: $"{_id}002",
        title: "Attribute can only be used in a union.",
        messageFormat: "The {0} attribute can only be used on types declared directly within a union type.",
        category: "SourceGenerator",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _attributeOnlyOnCaseTypeParameter = new(
        id: $"{_id}003",
        title: "Attribute can only be used on the type parameters of a union case.",
        messageFormat: "The {0} attribute can only be used on type parameters of types declared directly within a union type.",
        category: "SourceGenerator",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _attributeOnlyOnClassOrStruct = new(
        id: $"{_id}004",
        title: "Attribute can only be used on classes or structs.",
        messageFormat: "The {0} attribute can only be used on classes or structs.",
        category: "SourceGenerator",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _attributePropertyMustBeAnIdentifier = new(
        id: $"{_id}009",
        title: "Invalid property value.",
        messageFormat: "The {0} property value on {1} must be a legal identifier.",
        category: "SourceGenerator",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _attributePropertyMustBeSpecificType = new(
        id: $"{_id}010",
        title: "Invalid property value.",
        messageFormat: "The {0} property value on {1} must be a {2}.",
        category: "SourceGenerator",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _caseCannotBeARefStruct = new(
        id: $"{_id}011",
        title: "Union cases cannot be ref structs.",
        messageFormat: "Union cases cannot be ref structs.",
        category: "SourceGenerator",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _caseCannotBeLessAccessibleThanTheUnion = new(
        id: $"{_id}011",
        title: "Union cases cannot be less accessible than the union they are part of.",
        messageFormat: "The case {0} ({1}) is less accessible than its union {2} ({3}).",
        category: "SourceGenerator",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _caseIsConfiguredButAlsoExcluded = new(
        id: $"{_id}006",
        title: "Cases should not be configured and also excluded.",
        messageFormat: $"Union cases should not have both a {Constants.DUnionCaseAttribute} and {Constants.DUnionCaseExcludeAttribute}.",
        category: "SourceGenerator",
        defaultSeverity: CA.DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _duplicateAttribute = new(
            id: $"{_id}001",
        title: "Duplicate DUnion Attribute.",
        messageFormat: "Cannot declare the {0} attribute more than once.",
        category: "SourceGenerator",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _incompatibleTypeParameters = new(
        id: $"{_id}007",
        title: "Incompatible generic type constraints.",
        messageFormat: "The type parameter '{0}' on '{1}' is not compatible with the type parameter '{2}' on '{3}'. These type parameters map onto the same type parameter on their enclosing union, and so must have identical constraints.",
        category: "SourceGenerator",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _internalError = new(
        id: $"{_id}999",
        title: "Internal Error.",
        messageFormat: "Something went wrong while running the DUnion SourceGenerator. Exception: {0}",
        category: "SourceGenerator",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _unionCannotBeStatic = new(
        id: $"{_id}006",
        title: "Unions cannot be static.",
        messageFormat: "Unions cannot be static.",
        category: "SourceGenerator",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _unionCannotHaveABaseType = new(
        id: $"{_id}005",
        title: "Unions cannot have a base type.",
        messageFormat: "Unions cannot have a base type. They must be either a struct, or directly inherit from System.Object.",
        category: "SourceGenerator",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _unionCannotHaveInstanceAutoProperties = new(
        id: $"{_id}006",
        title: "Unions cannot have instance fields.",
        messageFormat: "Unions cannot have instance fields, which an auto property will add.",
        category: "SourceGenerator",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _unionCannotHaveInstanceFields = new(
        id: $"{_id}006",
        title: "Unions cannot have instance fields.",
        messageFormat: "Unions cannot have instance fields.",
        category: "SourceGenerator",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _unionConstructorMustCallAnotherConstructor = new(
        id: $"{_id}006",
        title: "Custom union constructors must call another constructor.",
        messageFormat: "Custom union constructors must call another constructor via `: this(...)`.",
        category: "SourceGenerator",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _unionsShouldHave2OrMoreCases = new(
                id: $"{_id}006",
        title: "Unions should have at least 2 cases.",
        messageFormat: "Unions should have at least 2 cases.",
        category: "SourceGenerator",
        defaultSeverity: CA.DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    public static void AttributeArgumentMustBeAnIdentifier(this GeneratorContext ctx, CA.AttributeData attribute, int index)
    {
        ctx.AddDiagnostics(_attributeArgumentMustBeAnIdentifier, [LocationOfCtorArgument(attribute, index)], [
            attribute.AttributeConstructor?.Parameters[index].Name ?? "name",
            attribute.AttributeClass?.ToFullSignature() ?? "UNKNOWN"
        ]);
    }

    public static void AttributeOnlyOnCase(this GeneratorContext ctx, string attributeName, IEnumerable<CA.AttributeData> attributes)
    {
        ctx.AddDiagnostics(_attributeOnlyOnCase, attributes.GetLocations(ctx.CancellationToken), [attributeName]);
    }

    public static void AttributeOnlyOnCaseTypeParameter(this GeneratorContext ctx, string attributeName, IEnumerable<CA.AttributeData> attributes)
    {
        ctx.AddDiagnostics(_attributeOnlyOnCaseTypeParameter, attributes.GetLocations(ctx.CancellationToken), [attributeName]);
    }

    public static void AttributeOnlyOnClassOrStruct(this GeneratorContext ctx, string attributeName, IEnumerable<CA.AttributeData> attributes)
    {
        ctx.AddDiagnostics(_attributeOnlyOnClassOrStruct, attributes.GetLocations(ctx.CancellationToken), [attributeName]);
    }

    public static void AttributePropertyMustBeAnIdentifier(this GeneratorContext ctx, CA.AttributeData attribute, string propertyName)
    {
        ctx.AddDiagnostics(_attributePropertyMustBeAnIdentifier, [LocationOfPropertyArgument(attribute, propertyName)], [
            propertyName,
            attribute.AttributeClass?.ToFullSignature() ?? "UNKNOWN"
        ]);
    }

    public static void AttributePropertyMustBeSpecificType(this GeneratorContext ctx, CA.AttributeData attribute, string propertyName, Type type)
    {
        ctx.AddDiagnostics(_attributePropertyMustBeSpecificType, [LocationOfPropertyArgument(attribute, propertyName)], [
            propertyName,
            attribute.AttributeClass?.ToFullSignature() ?? "UNKNOWN",
            type.ToString()
        ]);
    }

    public static void CaseCannotBeARefStruct(this GeneratorContext ctx, CA.INamedTypeSymbol @case)
    {
        var refKeyword = @case.DeclaringSyntaxReferences
            .Select(r => r.GetSyntax(ctx.CancellationToken))
            .OfType<CA.CSharp.Syntax.StructDeclarationSyntax>()
            .SelectMany(s => s.Modifiers)
            .Where(m => m.IsKind(CA.CSharp.SyntaxKind.RefKeyword))
            .Select(Location? (m) => m.GetLocation())
            .ToArray();

        ctx.AddDiagnostics(_caseCannotBeARefStruct, refKeyword.Length == 0 ? @case.GetLocations() : refKeyword, []);
    }

    public static void CaseCannotBeLessAccessibleThanTheUnion(this GeneratorContext ctx, CA.INamedTypeSymbol union, CA.INamedTypeSymbol @case)
    {
        ctx.AddDiagnostics(_caseCannotBeLessAccessibleThanTheUnion, @case.GetLocations(), [
            @case.ToFullSignature(),
            @case.DeclaredAccessibility.ToString(),
            union.ToFullSignature(),
            union.DeclaredAccessibility.ToString()
        ]);
    }

    public static void CaseIsConfiguredButAlsoExcluded(this GeneratorContext ctx, IEnumerable<CA.AttributeData> exclude, IEnumerable<CA.AttributeData> config)
    {
        ctx.AddDiagnostics(_caseIsConfiguredButAlsoExcluded, exclude.GetLocations(ctx.CancellationToken).Concat(config.GetLocations(ctx.CancellationToken)), []);
    }

    public static void DuplicateAttribute(this GeneratorContext ctx, string attributeName, IEnumerable<CA.AttributeData> attributes)
    {
        ctx.AddDiagnostics(_duplicateAttribute, attributes.GetLocations(ctx.CancellationToken), [attributeName]);
    }

    public static void IncompatibleTypeParameters(this GeneratorContext ctx, TypeId case1, TypeParameter case1Parameter, TypeId case2, TypeParameter case2Parameter)
    {
        ctx.AddDiagnostics(_incompatibleTypeParameters, case1Parameter.Location, [
            case1Parameter.Name,
            Helpers.ToFullName(case1),
            case2Parameter.Name,
            Helpers.ToFullName(case2)
        ]);
    }

    public static void InternalError(this GeneratorContext ctx, IEnumerable<Location?> locations, Exception ex)
    {
        ctx.AddDiagnostics(_internalError, locations, [ex.ToString()]);
    }

    public static void UnionCannotBeStatic(this GeneratorContext ctx, CA.INamedTypeSymbol union)
    {
        ctx.AddDiagnostics(_unionCannotBeStatic, union.GetLocations(), []);
    }

    public static void UnionCannotHaveABaseType(this GeneratorContext ctx, CA.INamedTypeSymbol unionType)
    {
        ctx.AddDiagnostics(_unionCannotHaveABaseType, unionType.GetLocations(), []);
    }

    public static void UnionCannotHaveInstanceAutoProperties(this GeneratorContext ctx, CA.IPropertySymbol property)
    {
        ctx.AddDiagnostics(_unionCannotHaveInstanceAutoProperties, property.GetLocations(), []);
    }

    public static void UnionCannotHaveInstanceFields(this GeneratorContext ctx, CA.IFieldSymbol field)
    {
        ctx.AddDiagnostics(_unionCannotHaveInstanceFields, field.GetLocations(), []);
    }

    public static void UnionConstructorMustCallAnotherConstructor(this GeneratorContext ctx, CA.IMethodSymbol ctor)
    {
        ctx.AddDiagnostics(_unionConstructorMustCallAnotherConstructor, ctor.GetLocations(), []);
    }

    public static void UnionsShouldHave2OrMoreCases(this GeneratorContext ctx, IEnumerable<CA.AttributeData> attributes)
    {
        ctx.AddDiagnostics(_unionsShouldHave2OrMoreCases, attributes.GetLocations(ctx.CancellationToken), []);
    }

    private static Location? GetLocation(this CA.AttributeData attribute, CancellationToken token)
    {
        return attribute.ApplicationSyntaxReference?.GetSyntax(token).GetLocation();
    }

    private static IEnumerable<Location?> GetLocations(this IEnumerable<CA.AttributeData> attributes, CancellationToken token)
    {
        return attributes.Select(a => GetLocation(a, token));
    }

    private static IEnumerable<Location?> GetLocations(this CA.ISymbol symbol)
    {
        return symbol.Locations.Select(l => (Location)l).ToSequence();
    }

    private static Location? LocationOfCtorArgument(CA.AttributeData? attribute, int index)
    {
        if (attribute?.ApplicationSyntaxReference?.GetSyntax() is not { } attrSyntax)
            return null;
        return (attrSyntax as CA.CSharp.Syntax.AttributeSyntax)
            ?.ArgumentList
            ?.Arguments
            .Where(a => a.NameEquals is null)
            .ElementAtOrDefault(index)
            ?.GetLocation()
            ?? attrSyntax.GetLocation();
    }

    private static Location? LocationOfPropertyArgument(CA.AttributeData? attribute, string name)
    {
        if (attribute?.ApplicationSyntaxReference?.GetSyntax() is not { } attrSyntax)
            return null;
        return (attrSyntax as CA.CSharp.Syntax.AttributeSyntax)
            ?.ArgumentList
            ?.Arguments
            .FirstOrDefault(a => a.NameEquals?.Name.ToString() == name)
            ?.GetLocation()
            ?? attrSyntax.GetLocation();
    }
}