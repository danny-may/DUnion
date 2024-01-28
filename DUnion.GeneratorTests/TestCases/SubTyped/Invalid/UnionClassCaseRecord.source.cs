using DUnion;

namespace SubTyped.Invalid.UnionClassCaseRecord;

[DUnion(Kind = UnionKind.SubType)]
public partial class Union
{
    public partial record class Case();
}