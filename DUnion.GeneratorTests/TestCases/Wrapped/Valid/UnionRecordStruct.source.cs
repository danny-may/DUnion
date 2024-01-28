using DUnion;

namespace Wrapped.Valid.UnionRecordStruct;

[DUnion]
public readonly partial record struct Union
{
    public record struct Case();
}