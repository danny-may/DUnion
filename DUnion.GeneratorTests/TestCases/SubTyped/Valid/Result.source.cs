using DUnion;

namespace SubTyped.Valid.Result;

[DUnion(Kind = UnionKind.SubType)]
public partial record class Result<TOk, TErr>
{
    public partial record class Ok(TOk Value);
    public partial record class Err(TErr Error);
}