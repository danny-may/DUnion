using DUnion;

namespace SubTyped.Valid.CaseRecordClass;

[DUnion(Kind = UnionKind.SubType)]
public partial record class Union 
{
    public partial record class Case();
}