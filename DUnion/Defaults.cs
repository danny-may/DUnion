using DUnion.Generators;
using DUnion.Models;
using DUnion.Readers;
using System.Collections.Generic;

namespace DUnion;

internal static class Defaults
{
    public static IUnionReader Reader => new UnionReader(Readers);

    public static Dictionary<UnionKind, INamedSymbolUnionReader> Readers => new()
    {
        [UnionKind.Wrapper] = new WrapperUnionReader(),
        [UnionKind.SubType] = new SubTypeUnionReader()
    };

    public static IStaticUnionGenerator StaticGenerator => new AttributeGenerator();
}