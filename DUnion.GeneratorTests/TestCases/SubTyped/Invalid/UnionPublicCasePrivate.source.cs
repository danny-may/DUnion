using DUnion;

namespace SubTyped.Invalid.UnionPublicCasePrivate;

[DUnion(Kind = UnionKind.SubType)]
public partial record class Union 
{
    private partial record class Ignored();
    public partial record class Case();
}