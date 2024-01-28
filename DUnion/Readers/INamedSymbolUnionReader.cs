using DUnion.Generators;
using DUnion.Models;
using CA = Microsoft.CodeAnalysis;

namespace DUnion.Readers;

internal interface INamedSymbolUnionReader
{
    IUnionGenerator Read(UnionConfig config, CA.INamedTypeSymbol symbol, CA.GeneratorAttributeSyntaxContext context);
}