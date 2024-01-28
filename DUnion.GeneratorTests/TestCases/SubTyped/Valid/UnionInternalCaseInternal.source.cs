using DUnion;

namespace SubTyped.Valid.UnionInternalCaseInternal;

[DUnion(Kind = UnionKind.SubType)]
internal partial record class Union
{
    internal partial record class Case();
}