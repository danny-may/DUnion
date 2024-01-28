using DUnion;

namespace SubTyped.Invalid.UnionRecordCaseClass;

[DUnion(Kind = UnionKind.SubType)]
public partial record class Union
{
    public partial class Case();
}