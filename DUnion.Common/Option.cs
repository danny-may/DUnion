namespace DUnion.Common;

[DUnion]
public readonly partial record struct Option<T>
{
    public readonly record struct Some(T Value);
    public readonly record struct None();
}

public static class Option
{
    public static Option<T> None<T>()
    {
        return new Option<T>.None();
    }

    public static Option<T> Some<T>(T value)
    {
        return new Option<T>.Some(value);
    }
}