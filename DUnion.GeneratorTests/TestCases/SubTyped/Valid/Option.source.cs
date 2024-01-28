using DUnion;

namespace SubTyped.Valid.Option;

[DUnion(Kind = UnionKind.SubType)]
public partial record class Option<T>
{
    public partial record class Some(T Value);
    public partial record class None();
}