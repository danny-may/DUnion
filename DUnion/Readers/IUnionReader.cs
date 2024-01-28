using DUnion.Generators;
using Microsoft.CodeAnalysis;

namespace DUnion.Readers;
internal interface IUnionReader
{
    IUnionGenerator Read(GeneratorAttributeSyntaxContext context);
}