using DUnion;

namespace Invalid.UnionPublicCaseProtected;

[DUnion]
public readonly partial record struct Union 
{
    protected readonly record struct Ignored();
    public readonly record struct Case();
}