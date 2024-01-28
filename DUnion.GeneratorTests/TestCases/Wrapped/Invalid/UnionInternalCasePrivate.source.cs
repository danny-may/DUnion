using DUnion;

namespace Wrapped.Invalid.UnionInternalCasePrivate;

[DUnion]
internal readonly partial record struct Union 
{
    private readonly record struct Ignored();
    public readonly record struct Case();
}