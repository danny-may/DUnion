using DUnion;

namespace Wrapped.Invalid.CaseRefStruct;

[DUnion]
public readonly partial record struct Union 
{
    public readonly ref struct Case();
}