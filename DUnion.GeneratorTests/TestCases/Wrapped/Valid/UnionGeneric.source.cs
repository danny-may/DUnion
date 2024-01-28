using DUnion;

namespace Wrapped.Valid.UnionGeneric;

[DUnion]
public readonly partial record struct Union<T>
{
    public record struct Case(T Value);
}