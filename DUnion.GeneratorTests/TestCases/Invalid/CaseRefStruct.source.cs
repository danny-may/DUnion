using DUnion;

namespace Invalid.CaseRefStruct;

[DUnion]
public readonly partial record struct Union 
{
    public readonly ref struct Case();
}