using DUnion;

namespace Valid.UnionInternalCaseInternal;

[DUnion]
internal readonly partial record struct Union
{
    internal record struct Case();
}