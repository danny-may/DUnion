using DUnion;

namespace Invalid.CaseGeneric;

[DUnion]
public readonly partial record struct Union 
{
    public readonly record struct Case<T>();
}