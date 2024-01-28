using DUnion.Models;
using CA = Microsoft.CodeAnalysis;

namespace DUnion.Generators;

internal sealed record DiagnosticUnionGenerator(IUnionGenerator Generator, Sequence<Diagnostic> Diagnostics) : IUnionGenerator
{
    public void Execute(CA.SourceProductionContext context)
    {
        foreach (var diagnostic in Diagnostics)
            context.ReportDiagnostic(diagnostic);
        Generator.Execute(context);
    }

    public bool Equals(IUnionGenerator other)
    {
        return Equals(other as DiagnosticUnionGenerator);
    }

    public Location Location => Generator.Location;
}