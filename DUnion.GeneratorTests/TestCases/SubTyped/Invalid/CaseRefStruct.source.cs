using DUnion;

namespace SubTyped.Invalid.CaseRefStruct;

[DUnion(Kind = UnionKind.SubType)]
public partial record class Union 
{
    public partial readonly ref struct Case();
}