using DUnion.Generators;
using DUnion.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace DUnion.Readers;

internal sealed class UnionReader : IUnionReader
{
    private static readonly Regex _identifier = new(@"^(?:((?!\d)\w+(?:\.(?!\d)\w+)*)\.)?((?!\d)\w+)$", RegexOptions.Compiled);
    private readonly ImmutableDictionary<UnionKind, INamedSymbolUnionReader> _readers;

    public UnionReader(IEnumerable<KeyValuePair<UnionKind, INamedSymbolUnionReader>> readers)
    {
        _readers = readers.ToImmutableDictionary();
    }

    public IUnionGenerator Read(GeneratorAttributeSyntaxContext context)
    {
        var location = context.TargetNode.GetLocation();
        if (context.TargetSymbol is not INamedTypeSymbol target)
            return new EmptyUnionGenerator(location);
        if (target.TypeKind is not (TypeKind.Struct or TypeKind.Class))
            return new EmptyUnionGenerator(location).WithDiagnostics([Diagnostics.NotAClassOrStruct(target)]);

        var diagnostics = new List<Models.Diagnostic>();
        if (!TryReadConfig(context, diagnostics.Add, out var config, out var reader))
            return new EmptyUnionGenerator(location).WithDiagnostics(diagnostics);

        return reader.Read(config, target, context);
    }

    private static bool TryConvertToUnionType(object? value, out UnionKind type)
    {
        switch (value)
        {
            case byte i: type = (UnionKind)i; return true;
            case sbyte i: type = (UnionKind)i; return true;
            case short i: type = (UnionKind)i; return true;
            case ushort i: type = (UnionKind)i; return true;
            case int i: type = (UnionKind)i; return true;
            case uint i: type = (UnionKind)i; return true;
            case long i: type = (UnionKind)i; return true;
            case ulong i: type = (UnionKind)i; return true;
            case nint i: type = (UnionKind)i; return true;
            case nuint i: type = (UnionKind)i; return true;
            case string s: return Enum.TryParse(s, out type);
            default: type = default; return false;
        }
    }

    private static bool TryFindNamedArgument(AttributeData attribute, string name, out AttributeValueResult result)
    {
        foreach (var kvp in attribute.NamedArguments)
        {
            if (kvp.Key != name)
                continue;

            var node = (attribute.ApplicationSyntaxReference
                ?.GetSyntax() as AttributeSyntax)
                ?.ArgumentList
                ?.Arguments
                .Where(arg => arg.NameEquals?.Name.ToString() == name)
                .Select(arg => arg.Expression)
                .FirstOrDefault();

            result = new AttributeValueResult(kvp.Value, node);
            return true;
        }

        result = default;
        return false;
    }

    private bool TryReadConfig(GeneratorAttributeSyntaxContext context, Action<Models.Diagnostic> report, out UnionConfig config, [NotNullWhen(true)] out INamedSymbolUnionReader? reader)
    {
        if (context.Attributes.Length == 0)
        {
            config = default;
            reader = null;
            return false;
        }

        if (context.Attributes.Length > 1)
            report(Diagnostics.MultipleAttributes(context.Attributes.Skip(1)));

        var attribute = context.Attributes[0];
        var results = new[]
        {
            TryReadUnionType(attribute, report, out reader),
            TryReadNamedIdentifierArgument(attribute, Constants.DUnionDiscriminator, report, out var discriminator),
            TryReadNamedIdentifierArgument(attribute, Constants.DUnionUnderlyingValue, report, out var underlyingValue),
            TryReadNamedIdentifierArgument(attribute, Constants.DUnionSwitch, report, out var @switch),
            TryReadNamedIdentifierArgument(attribute, Constants.DUnionMatch, report, out var match),
            TryReadIsCaseName(attribute, report, out var @case),
        };
        config = new(discriminator, underlyingValue, @switch, match, @case);
        return !results.Contains(false) && reader is not null;
    }

    private bool TryReadIsCaseName(AttributeData attribute, Action<Models.Diagnostic> report, out string? identifier)
    {
        if (!TryFindNamedArgument(attribute, Constants.DUnionIsCase, out var prop))
        {
            identifier = null;
            return true;
        }

        identifier = prop.Value.Value?.ToString() ?? "";
        if (!_identifier.IsMatch(identifier.Replace("{0}", "CaseNameGoesHere")))
        {
            report(Diagnostics.InvalidIdentifier(attribute, prop, Constants.DUnionIsCase, identifier));
            return false;
        }

        if (!identifier.Contains("{0}"))
            identifier += "{0}";

        return true;
    }

    private bool TryReadNamedIdentifierArgument(AttributeData attribute, string name, Action<Models.Diagnostic> report, out string? identifier)
    {
        if (!TryFindNamedArgument(attribute, name, out var prop))
        {
            identifier = null;
            return true;
        }

        identifier = prop.Value.Value?.ToString() ?? "";
        if (!_identifier.IsMatch(identifier))
        {
            report(Diagnostics.InvalidIdentifier(attribute, prop, name, identifier));
            return false;
        }

        return true;
    }

    private bool TryReadUnionType(AttributeData attribute, Action<Models.Diagnostic> report, [NotNullWhen(true)] out INamedSymbolUnionReader? reader)
    {
        if (!TryFindNamedArgument(attribute, Constants.DUnionType, out var prop))
        {
            reader = _readers.Values.First();
            return true;
        }
        if (!TryConvertToUnionType(prop.Value.Value, out var type))
        {
            report(Diagnostics.InvalidType(attribute, prop));
            reader = null;
            return false;
        }

        if (!_readers.TryGetValue(type, out reader))
        {
            report(Diagnostics.NotSupportedUnionType(attribute, prop, type));
            return false;
        }
        return true;
    }
}