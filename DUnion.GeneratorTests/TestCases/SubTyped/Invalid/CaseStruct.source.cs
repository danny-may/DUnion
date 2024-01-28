using DUnion;

namespace SubTyped.Invalid.CaseStruct;

[DUnion(Kind = UnionKind.SubType)]
public partial record class Union 
{
    public partial struct Case();
}