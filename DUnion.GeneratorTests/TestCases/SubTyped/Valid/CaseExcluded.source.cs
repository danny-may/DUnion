using DUnion;

namespace SubTyped.Valid.CaseExcluded;

[DUnion(Kind = UnionKind.SubType)]
public partial record class Union 
{
    [DUnionExclude]
    public partial record class Ignored();
    public partial record class Case();
}