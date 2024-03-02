using DUnion;

namespace Invalid.UnionPublicCaseInternal;

[DUnion]
public readonly partial record struct Union 
{
    internal readonly record struct Ignored();
    public readonly record struct Case();
}