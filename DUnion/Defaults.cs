using DUnion.Generators;
using DUnion.Readers;

namespace DUnion;

internal static class Defaults
{
    public static IUnionReader Reader => new UnionReader(new WrapperUnionReader());

    public static IStaticUnionGenerator StaticGenerator => new AttributeGenerator();
}