using DUnion;

namespace Wrapped.Invalid.UnionInternalCaseProtected;

[DUnion]
internal readonly partial record struct Union 
{
    protected readonly record struct Ignored();
    public readonly record struct Case();
}