using DUnion;

namespace SubTyped.Invalid.CaseGeneric;

[DUnion(Kind = UnionKind.SubType)]
public partial record class Union 
{
    public partial record class Case<T>();
}