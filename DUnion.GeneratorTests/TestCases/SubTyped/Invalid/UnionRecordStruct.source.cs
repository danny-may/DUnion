using DUnion;

namespace SubTyped.Invalid.UnionRecordStruct;

[DUnion(Kind = UnionKind.SubType)]
public readonly partial record struct Union
{
    public partial readonly record struct Case();
}