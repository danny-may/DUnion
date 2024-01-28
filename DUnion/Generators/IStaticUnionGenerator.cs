using CA = Microsoft.CodeAnalysis;

namespace DUnion.Generators;

internal interface IStaticUnionGenerator
{
    void Execute(CA.IncrementalGeneratorPostInitializationContext context);
}