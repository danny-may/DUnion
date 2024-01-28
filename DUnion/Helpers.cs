using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DUnion;

internal static class Helpers
{
    public static IEnumerable<INamedTypeSymbol> ContainingTypes(this ISymbol symbol)
    {
        for (var container = symbol.ContainingType; container is not null; container = container.ContainingType)
        {
            yield return container;
        }
    }

    public static string GetNamespace(this ISymbol symbol)
    {
        var namespaceStack = new List<INamespaceSymbol>();
        for (var current = symbol.ContainingNamespace; current is not null; current = current.ContainingNamespace)
            namespaceStack.Add(current);
        var @namespace = string.Join(".", namespaceStack.Select(n => n.Name).Where(n => n.Length > 0).Reverse());
        return @namespace;
    }

    public static string ToFullSignature(this INamedTypeSymbol symbol)
    {
        var result = new StringBuilder();
        result.Append(symbol.GetNamespace());
        foreach (var container in symbol.ContainingTypes().Reverse())
        {
            result.Append(".");
            ToSignature(container, result);
        }
        result.Append(".");
        ToSignature(symbol, result);
        return result.ToString().TrimStart('.');
    }

    public static string ToSignature(this INamedTypeSymbol symbol)
    {
        var result = new StringBuilder();
        ToSignature(symbol, result);
        return result.ToString();
    }

    private static void ToSignature(INamedTypeSymbol symbol, StringBuilder result)
    {
        result.Append(symbol.Name);
        if (symbol.TypeParameters is { Length: > 0 })
        {
            result.Append("<");
            result.Append(symbol.TypeParameters[0].Name);
            foreach (var parameter in symbol.TypeParameters.Skip(1))
            {
                result.Append(",");
                result.Append(parameter.Name);
            }
            result.Append(">");
        }
    }
}