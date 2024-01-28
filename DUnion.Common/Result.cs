namespace DUnion.Common;

[DUnion]
public readonly partial record struct Result<TOk, TErr>
{
    public readonly record struct Ok(TOk Value);
    public readonly record struct Err(TErr Error);

    public static implicit operator Result<TOk, TErr>(Result.OkResultValue<TOk> value)
    {
        return new(new Ok(value.Value));
    }

    public static implicit operator Result<TOk, TErr>(Result.ErrResultError<TErr> value)
    {
        return new(new Err(value.Error));
    }
}

public static class Result
{
    public readonly record struct OkResultValue<TOk>(TOk Value);

    public readonly record struct ErrResultError<TErr>(TErr Error);

    public static ErrResultError<TErr> Err<TErr>(TErr error)
    {
        return new(error);
    }

    public static OkResultValue<TOk> Ok<TOk>(TOk value)
    {
        return new(value);
    }
}