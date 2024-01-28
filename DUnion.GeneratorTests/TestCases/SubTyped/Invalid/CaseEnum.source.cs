using DUnion;

namespace SubTyped.Invalid.CaseEnum;

[DUnion(Kind = UnionKind.SubType)]
public partial record class Union 
{
    public enum Case { }
}