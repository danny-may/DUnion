using DUnion;

namespace Wrapped.Valid.CaseDelegate;

[DUnion]
public readonly partial record struct Union 
{
    public delegate void Case();
}