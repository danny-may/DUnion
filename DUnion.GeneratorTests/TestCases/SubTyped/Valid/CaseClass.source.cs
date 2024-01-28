using DUnion;

namespace SubTyped.Valid.CaseClass;

[DUnion(Kind = UnionKind.SubType)]
public partial class Union
{
    public partial class Case
    {
    }
}