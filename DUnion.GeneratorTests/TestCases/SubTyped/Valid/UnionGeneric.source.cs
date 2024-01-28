using DUnion;

namespace SubTyped.Valid.UnionGeneric;

[DUnion(Kind = UnionKind.SubType)]
public partial record class Union<T>
{
    public partial record class Case(T Value);
}