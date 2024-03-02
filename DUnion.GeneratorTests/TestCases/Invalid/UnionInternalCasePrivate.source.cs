using DUnion;

namespace Invalid.UnionInternalCasePrivate;

[DUnion]
internal readonly partial record struct Union 
{
    private readonly record struct Ignored();
    public readonly record struct Case();
}