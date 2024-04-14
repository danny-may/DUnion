using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using CA = Microsoft.CodeAnalysis;

namespace DUnion.Models;

internal sealed class GeneratorContext : IEquatable<GeneratorContext>
{
    private readonly List<Diagnostic> _diagnostics = [];

    public CancellationToken CancellationToken { get; }

    public CA.SemanticModel Model { get; }

    public GeneratorContext(CA.SemanticModel model, CancellationToken cancellationToken)
    {
        Model = model;
        CancellationToken = cancellationToken;
    }

    public void AddDiagnostic(CA.DiagnosticDescriptor descriptor, IEnumerable<Location?> locations, IEnumerable<string> messageArgs)
    {
        _diagnostics.Add(new(
            descriptor,
            locations.Where(x => x is not null).ToSequence()!,
            messageArgs.ToSequence()));
    }

    public bool Equals(GeneratorContext other)
    {
        return other is not null
            && other._diagnostics.Count == _diagnostics.Count
            && _diagnostics.ToImmutableHashSet().SetEquals(other._diagnostics);
    }

    public void SendDiagnostics(CA.SourceProductionContext context)
    {
        foreach (var diagnostic in _diagnostics)
        {
            context.ReportDiagnostic(diagnostic);
        }
    }
}