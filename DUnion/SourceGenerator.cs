using Microsoft.CodeAnalysis;

namespace DUnion;

[Generator]
internal sealed class SourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        GeneratorOutputs.StaticFiles(context);
        GeneratorOutputs.Unions(context);
        GeneratorOutputs.ExcludeNonUnionCases(context);
        GeneratorOutputs.ExplicitNonUnionCases(context);
        GeneratorOutputs.GenericNonUnionCaseTypeParameters(context);
    }
}