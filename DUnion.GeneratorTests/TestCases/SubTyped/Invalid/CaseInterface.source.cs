using DUnion;

namespace SubTyped.Invalid.CaseInterface;

[DUnion(Kind = UnionKind.SubType)]
public partial record class Union 
{
    public partial interface ICase { }
}