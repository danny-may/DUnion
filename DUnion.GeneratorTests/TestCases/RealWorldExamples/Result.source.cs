using DUnion;

namespace TestCases;

[DUnion]
public static class Result
{
    public readonly record struct Ok<TOk>(TOk Value);
    public readonly record struct Err<TErr>(TErr Error);
}