using DUnion.Models;
using CA = Microsoft.CodeAnalysis;

namespace DUnion.Generators;

internal sealed record EmptyUnionGenerator(Location Location) : IUnionGenerator
{
    public void Execute(CA.SourceProductionContext context)
    {
    }

    public bool Equals(IUnionGenerator other)
    {
        return Equals(other as EmptyUnionGenerator);
    }
}