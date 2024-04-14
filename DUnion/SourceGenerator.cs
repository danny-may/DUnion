using CA = Microsoft.CodeAnalysis;

namespace DUnion;

[CA.Generator]
internal sealed class SourceGenerator : CA.IIncrementalGenerator
{
    public void Initialize(CA.IncrementalGeneratorInitializationContext context)
    {
        GeneratorOutputs.StaticFiles(context);
        GeneratorOutputs.Unions(context);
        GeneratorOutputs.ExcludeNonUnionCases(context);
        GeneratorOutputs.ExplicitNonUnionCases(context);
        GeneratorOutputs.GenericNonUnionCaseTypeParameters(context);
    }
}