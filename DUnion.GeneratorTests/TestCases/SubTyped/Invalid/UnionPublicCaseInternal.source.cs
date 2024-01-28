using DUnion;

namespace SubTyped.Invalid.UnionPublicCaseInternal;

[DUnion(Kind = UnionKind.SubType)]
public partial record class Union 
{
    internal partial record class Ignored();
    public partial record class Case();
}