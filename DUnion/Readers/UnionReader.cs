using DUnion.Generators;
using DUnion.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

namespace DUnion.Readers;

internal sealed class UnionReader : IUnionReader
{
    private static readonly Regex _identifier = new(@"^(?:((?!\d)\w+(?:\.(?!\d)\w+)*)\.)?((?!\d)\w+)$", RegexOptions.Compiled);
    private readonly INamedSymbolUnionReader _reader;

    public UnionReader(INamedSymbolUnionReader reader)
    {
        _reader = reader;
    }

    public IUnionGenerator Read(GeneratorAttributeSyntaxContext context)
    {
        var location = context.TargetNode.GetLocation();
        if (context.TargetSymbol is not INamedTypeSymbol target)
            return new EmptyUnionGenerator(location);
        if (target.TypeKind is not (TypeKind.Struct or TypeKind.Class))
            return new EmptyUnionGenerator(location).WithDiagnostics([Diagnostics.NotAClassOrStruct(target)]);

        var diagnostics = new List<Models.Diagnostic>();
        if (!TryReadConfig(context, diagnostics.Add, out var config))
            return new EmptyUnionGenerator(location).WithDiagnostics(diagnostics);

        return _reader.Read(config, target, context);
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

    private bool TryReadConfig(GeneratorAttributeSyntaxContext context, Action<Models.Diagnostic> report, out UnionConfig config)
    {
        if (context.Attributes.Length == 0)
        {
            config = default;
            return false;
        }

        if (context.Attributes.Length > 1)
            report(Diagnostics.MultipleAttributes(context.Attributes.Skip(1)));

        var attribute = context.Attributes[0];
        var results = new[]
        {
            TryReadNamedIdentifierArgument(attribute, Constants.DUnionDiscriminator, report, out var discriminator),
            TryReadNamedIdentifierArgument(attribute, Constants.DUnionUnderlyingValue, report, out var underlyingValue),
            TryReadNamedIdentifierArgument(attribute, Constants.DUnionSwitch, report, out var @switch),
            TryReadNamedIdentifierArgument(attribute, Constants.DUnionMatch, report, out var match),
            TryReadIsCaseName(attribute, report, out var @case),
        };
        config = new(discriminator, underlyingValue, @switch, match, @case);
        return !results.Contains(false);
    }

    private bool TryReadIsCaseName(AttributeData attribute, Action<Models.Diagnostic> report, out string? identifier)
    {
        if (!TryFindNamedArgument(attribute, Constants.DUnionIsCase, out var prop))
        {
            identifier = null;
            return true;
        }

        identifier = prop.Value.Value?.ToString() ?? "";
        try
        {
            var isCaseResult = string.Format(identifier, "CaseNameGoesHere");
            if (!_identifier.IsMatch(isCaseResult))
            {
                report(Diagnostics.InvalidIdentifier(attribute, prop, Constants.DUnionIsCase, identifier));
                return false;
            }
        }
        catch (FormatException)
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
}