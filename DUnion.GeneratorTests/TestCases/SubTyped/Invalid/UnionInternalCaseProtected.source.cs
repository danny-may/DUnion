using DUnion;

namespace SubTyped.Invalid.UnionInternalCaseProtected;

[DUnion(Kind = UnionKind.SubType)]
internal partial record class Union 
{
    protected partial record class Ignored();
    public partial record class Case();
}