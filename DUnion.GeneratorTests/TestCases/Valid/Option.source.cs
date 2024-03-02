using DUnion;

namespace Valid.Option;

[DUnion]
public readonly partial record struct Option<T>
{
    public readonly record struct Some(T Value);
    public readonly record struct None();
}