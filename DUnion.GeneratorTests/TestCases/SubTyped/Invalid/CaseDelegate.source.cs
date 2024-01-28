using DUnion;

namespace SubTyped.Invalid.CaseDelegate;

[DUnion(Kind = UnionKind.SubType)]
public partial record class Union 
{
    public delegate void Case();
}