using DUnion;

namespace SubTyped.Invalid.CaseRecordStruct;

[DUnion(Kind = UnionKind.SubType)]
public readonly partial record struct Union 
{
    public partial record struct Case();
}