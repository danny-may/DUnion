namespace DUnion.Common;

[DUnion]
public static class Option
{
    public readonly record struct Some<T>(T Value);
    public readonly record struct None();
}