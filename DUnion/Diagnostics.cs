using DUnion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using CA = Microsoft.CodeAnalysis;

namespace DUnion;

internal static class Diagnostics
{
    private const string _id = "DYNUNION";

    private static readonly string _0supportedTypes = $"Supported values are: {string.Join(", ", Enum.GetValues(typeof(UnionKind)).Cast<UnionKind>().Select(v => $"{nameof(UnionKind)}.{v}"))}";

    private static readonly CA.DiagnosticDescriptor _cannotSetBaseTypeOfNonClass = new(
        id: $"{_id}001",
        title: $"Invalid case type",
        messageFormat: $"All cases of a {nameof(UnionKind.SubType)} union must be able to inherit from the union type, but '{{0}}' is a {{1}} and so cannot inherit from '{{2}}'.",
        category: "Usage",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _cannotSetNonRecordBaseTypeRecord = new(
        id: $"{_id}002",
        title: $"Invalid case type",
        messageFormat: $"All cases of a {nameof(UnionKind.SubType)} union must be able to inherit from the union type, but '{{0}}' is not a record and so cannot inherit from '{{1}}' as it is a record.",
        category: "Usage",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _cannotSetRecordBaseTypeNonRecord = new(
        id: $"{_id}003",
        title: $"Invalid case type",
        messageFormat: $"All cases of a {nameof(UnionKind.SubType)} union must be able to inherit from the union type, but '{{0}}' is a record and so cannot inherit from '{{1}}' as it is not a record.",
        category: "Usage",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _cannotSubTypeSealed = new(
        id: $"{_id}004",
        title: $"Invalid union type",
        messageFormat: $"All cases of a {nameof(UnionKind.SubType)} union must be able to inherit from the union type, but '{{0}}' is marked as sealed meaning nothing can inherit from it.",
        category: "Usage",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _cannotSubTypeWhenBaseTypeAlreadySet = new(
        id: $"{_id}005",
        title: $"Invalid case type",
        messageFormat: $"All cases of a {nameof(UnionKind.SubType)} union must be able to inherit from the union type, but '{{0}}' already inherits from '{{1}}' and so cannot inhertit from '{{2}}'.",
        category: "Usage",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _cannotUseGenericCase = new(
        id: $"{_id}06",
        title: $"Case cannot be generic",
        messageFormat: $"The '{{0}}' case cannot be generic. All generic type arguments must live on the '{{1}}' union",
        category: "Usage",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _cannotUseNonClassAsBaseType = new(
        id: $"{_id}007",
        title: $"Invalid union type",
        messageFormat: $"All cases of a {nameof(UnionKind.SubType)} union must be able to inherit from the union type, but '{{0}}' is a {{1}} meaning nothing can inherit from it.",
        category: "Usage",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _caseCannotBeARefStruct = new(
        id: $"{_id}008",
        title: $"Case cannot be a ref struct",
        messageFormat: $"The '{{0}}' case is a ref struct, which cannot be used in a {{1}} union.",
        category: "Usage",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _emptyUnion = new(
        id: $"{_id}009",
        title: $"Empty union",
        messageFormat: $"The '{{0}}' union has no cases.",
        category: "Usage",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _ignoringLessAccessibleType = new(
        id: $"{_id}500",
        title: $"Union case less accessible than union",
        messageFormat: $"The '{{0}}' case is less accessible than the '{{1}}' union which contains it, and so is not part of the union.",
        category: "Usage",
        defaultSeverity: CA.DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _internalError = new(
        id: $"{_id}499",
        title: "Internal Error",
        messageFormat: "Something went wrong while running the DUnion SourceGenerator. Exception:{0}",
        category: "SourceGenerator",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _invalidIdentifier = new(
        id: $"{_id}010",
        title: $"Invalid Identifier",
        messageFormat: $"The value '{{0}}' is not a valid name for the {{1}}. {{1}} must be a valid C# identifier name.",
        category: "Usage",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _invalidType = new(
        id: $"{_id}011",
        title: $"Invalid {nameof(UnionKind)}",
        messageFormat: $"The value '{{0}}' is not a valid {nameof(UnionKind)}. {_0supportedTypes}",
        category: "Usage",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _invalidUnderlyingValue = new(
        id: $"{_id}012",
        title: $"Invalid UnderlyingValue",
        messageFormat: $"The value '{{0}}' is not a valid name for the underlying value. UnderlyingValue must be a valid C# identifier name.",
        category: "Usage",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _multipleAttributes = new(
        id: $"{_id}013",
        title: $"Multiple {Constants.DUnionFullName}s",
        messageFormat: $"The {Constants.DUnionFullName} may only appear once on any given type.",
        category: "Usage",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _notClassOrStruct = new(
        id: $"{_id}014",
        title: $"Invalid DUnion target",
        messageFormat: $"The [DUnion] attribute can only be applied to classes and structs, not {{0}}.",
        category: "Usage",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static Diagnostic InvalidType(CA.AttributeData attribute, AttributeValueResult data)
    {
        var syntax = data.Node ?? attribute.ApplicationSyntaxReference?.GetSyntax();
        var locations = syntax is null ? [] : new Sequence<Location>([syntax.GetLocation()]);

        return new(_invalidType, locations, new([
            data.Node?.ToString() ?? data.Value.ToString() ?? "null"
        ]));
    }

    public static Diagnostic MultipleAttributes(IEnumerable<CA.AttributeData> attributes)
    {
        var locations = attributes
            .Select(a => a.ApplicationSyntaxReference?.GetSyntax().GetLocation())
            .Where(l => l is not null)
            .Select(l => (Location)l!);
        return new(_multipleAttributes, new(locations), []);
    }

    public static Diagnostic NotSupportedUnionType(CA.AttributeData attribute, AttributeValueResult value, UnionKind actual)
    {
        var syntax = value.Node ?? attribute.ApplicationSyntaxReference?.GetSyntax();
        var locations = syntax is not null
            ? new Sequence<Location>([syntax.GetLocation()])
            : [];

        return new(_invalidType, locations, new([
            actual.ToString()
        ]));
    }

    internal static Diagnostic CannotSetBaseTypeOfNonClass(CA.INamedTypeSymbol caseSymbol, CA.INamedTypeSymbol unionSymbol)
    {
        var locations = LocationsOf(caseSymbol);
        return new(_cannotSetBaseTypeOfNonClass, locations, new([
            caseSymbol.ToFullSignature(),
            caseSymbol.TypeKind.ToString(),
            unionSymbol.ToFullSignature()
        ]));
    }

    internal static Diagnostic CannotSetNonRecordBaseTypeRecord(CA.INamedTypeSymbol caseSymbol, CA.INamedTypeSymbol unionSymbol)
    {
        var locations = LocationsOf(caseSymbol);
        return new(_cannotSetNonRecordBaseTypeRecord, locations, new([
            caseSymbol.ToFullSignature(),
            unionSymbol.ToFullSignature()
        ]));
    }

    internal static Diagnostic CannotSetRecordBaseTypeNonRecord(CA.INamedTypeSymbol caseSymbol, CA.INamedTypeSymbol unionSymbol)
    {
        var locations = LocationsOf(caseSymbol);
        return new(_cannotSetRecordBaseTypeNonRecord, locations, new([
            caseSymbol.ToFullSignature(),
            unionSymbol.ToFullSignature()
        ]));
    }

    internal static Diagnostic CannotSubTypeSealed(CA.INamedTypeSymbol unionSymbol)
    {
        var locations = LocationsOf(unionSymbol);
        return new(_cannotSubTypeSealed, locations, new([
            unionSymbol.ToFullSignature()
        ]));
    }

    internal static Diagnostic CannotSubTypeWhenBaseTypeAlreadySet(CA.INamedTypeSymbol caseSymbol, CA.INamedTypeSymbol unionSymbol)
    {
        var locations = LocationsOf(caseSymbol);
        return new(_cannotSubTypeWhenBaseTypeAlreadySet, locations, new([
            caseSymbol.ToFullSignature(),
            caseSymbol.BaseType?.ToFullSignature() ?? "object",
            unionSymbol.ToFullSignature()
        ]));
    }

    internal static Diagnostic CannotUseGenericCase(CA.INamedTypeSymbol caseSymbol, CA.INamedTypeSymbol unionSymbol)
    {
        var locations = LocationsOf(caseSymbol);
        return new(_cannotUseGenericCase, locations, new([
            caseSymbol.ToFullSignature(),
            unionSymbol.ToFullSignature()
        ]));
    }

    internal static Diagnostic CannotUseNonClassAsBaseType(CA.INamedTypeSymbol unionSymbol)
    {
        var locations = LocationsOf(unionSymbol);
        return new(_cannotUseNonClassAsBaseType, locations, new([
            unionSymbol.ToFullSignature(),
            unionSymbol.TypeKind.ToString()
        ]));
    }

    internal static Diagnostic CaseCannotBeARefStruct(CA.INamedTypeSymbol caseSymbol, UnionKind type)
    {
        var locations = LocationsOf(caseSymbol);
        return new(_caseCannotBeARefStruct, locations, new([
            caseSymbol.ToFullSignature(),
            type.ToString()
        ]));
    }

    internal static Diagnostic EmptyUnion(CA.INamedTypeSymbol unionSymbol)
    {
        var locations = LocationsOf(unionSymbol);
        return new(_emptyUnion, locations, new([
            unionSymbol.ToFullSignature()
        ]));
    }

    internal static Diagnostic IgnoringLessAccessibleType(CA.INamedTypeSymbol caseSymbol, CA.INamedTypeSymbol unionSymbol)
    {
        var locations = LocationsOf(caseSymbol);
        return new(_ignoringLessAccessibleType, locations, new([
            caseSymbol.ToFullSignature(),
            unionSymbol.ToFullSignature()
        ]));
    }

    internal static Diagnostic InternalError(Location? location, Exception ex)
    {
        return new(_internalError, new(location is null ? [] : [location.Value]), new([
            ex.ToString().Replace("\n", "<br />")
        ]));
    }

    internal static Diagnostic InvalidIdentifier(CA.AttributeData attribute, AttributeValueResult prop, string name, string value)
    {
        var syntax = prop.Node ?? attribute.ApplicationSyntaxReference?.GetSyntax();
        var locations = syntax is not null
            ? new Sequence<Location>([syntax.GetLocation()])
            : [];

        return new(_invalidIdentifier, locations, new([value, name]));
    }

    internal static Diagnostic NotAClassOrStruct(CA.INamedTypeSymbol targetSymbol)
    {
        var locations = LocationsOf(targetSymbol);
        return new(_notClassOrStruct, locations, new([targetSymbol.TypeKind.ToString()]));
    }

    private static Sequence<Location> LocationsOf(CA.ISymbol symbol)
    {
        return new(symbol.Locations.Select(l => (Location)l));
    }
}