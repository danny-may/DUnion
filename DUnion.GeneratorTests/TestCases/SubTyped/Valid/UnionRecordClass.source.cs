using DUnion;

namespace SubTyped.Valid.UnionRecordClass;

[DUnion(Kind = UnionKind.SubType)]
public partial record class Union 
{
    public partial record class Case();
}