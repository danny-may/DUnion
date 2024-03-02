using DUnion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using CA = Microsoft.CodeAnalysis;

namespace DUnion;

internal static class Diagnostics
{
    private const string _id = "DYNUNION";

    private static readonly CA.DiagnosticDescriptor _cannotUseGenericCase = new(
        id: $"{_id}06",
        title: $"Case cannot be generic",
        messageFormat: $"The '{{0}}' case cannot be generic. All generic type arguments must live on the '{{1}}' union",
        category: "Usage",
        defaultSeverity: CA.DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly CA.DiagnosticDescriptor _caseCannotBeARefStruct = new(
        id: $"{_id}008",
        title: $"Case cannot be a ref struct",
        messageFormat: $"The '{{0}}' case is a ref struct, which cannot be used in a discriminated union.",
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

    public static Diagnostic MultipleAttributes(IEnumerable<CA.AttributeData> attributes)
    {
        var locations = attributes
            .Select(a => a.ApplicationSyntaxReference?.GetSyntax().GetLocation())
            .Where(l => l is not null)
            .Select(l => (Location)l!);
        return new(_multipleAttributes, new(locations), []);
    }

    internal static Diagnostic CannotUseGenericCase(CA.INamedTypeSymbol caseSymbol, CA.INamedTypeSymbol unionSymbol)
    {
        var locations = LocationsOf(caseSymbol);
        return new(_cannotUseGenericCase, locations, new([
            caseSymbol.ToFullSignature(),
            unionSymbol.ToFullSignature()
        ]));
    }

    internal static Diagnostic CaseCannotBeARefStruct(CA.INamedTypeSymbol caseSymbol)
    {
        var locations = LocationsOf(caseSymbol);
        return new(_caseCannotBeARefStruct, locations, new([
            caseSymbol.ToFullSignature()
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