using DUnion;

namespace Valid.CaseDelegate;

[DUnion]
public readonly partial record struct Union 
{
    public delegate void Case();
}