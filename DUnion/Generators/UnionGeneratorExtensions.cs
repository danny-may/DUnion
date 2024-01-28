using DUnion.Models;
using System.Collections.Generic;

namespace DUnion.Generators;

internal static class UnionGeneratorExtensions
{
    public static IUnionGenerator WithDiagnostics(this IUnionGenerator generator, IEnumerable<Diagnostic> diagnostics)
    {
        return new DiagnosticUnionGenerator(generator, new(diagnostics));
    }
}