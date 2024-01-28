using DUnion;

namespace SubTyped.Invalid.UnionPublicCaseProtected;

[DUnion(Kind = UnionKind.SubType)]
public partial record class Union 
{
    protected partial record class Ignored();
    public partial record class Case();
}