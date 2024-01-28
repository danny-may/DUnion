using DUnion;

namespace SubTyped.Invalid.UnionInternalCasePrivate;

[DUnion(Kind = UnionKind.SubType)]
internal partial record class Union 
{
    private partial record class Ignored();
    public partial record class Case();
}