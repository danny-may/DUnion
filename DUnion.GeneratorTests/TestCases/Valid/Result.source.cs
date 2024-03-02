using DUnion;

namespace Valid.Result;

[DUnion]
public readonly partial record struct Result<TOk, TErr>
{
    public readonly record struct Ok(TOk Value);
    public readonly record struct Err(TErr Error);
}