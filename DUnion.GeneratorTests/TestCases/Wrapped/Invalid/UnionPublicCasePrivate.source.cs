using DUnion;

namespace Wrapped.Invalid.UnionPublicCasePrivate;

[DUnion]
public readonly partial record struct Union 
{
    private readonly record struct Ignored();
    public readonly record struct Case();
}