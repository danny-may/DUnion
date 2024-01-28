using DUnion;

namespace Wrapped.Invalid.CaseGeneric;

[DUnion]
public readonly partial record struct Union 
{
    public readonly record struct Case<T>();
}