using DUnion;

namespace Wrapped.Valid.CaseExcluded;

[DUnion]
public readonly partial record struct Union 
{
    [DUnionExclude]
    public readonly record struct Ignored();
    public readonly record struct Case();
}