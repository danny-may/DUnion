using DUnion;

namespace SubTyped.Invalid.UnionStruct;

[DUnion(Kind = UnionKind.SubType)]
public readonly partial struct Union
{
    public readonly partial struct Case();
}