namespace DUnion.Models;

internal readonly record struct Union(
    string? Namespace,
    Sequence<TypeRef> ContainingTypes,
    TypeRef Type,
    UnionConfig Config,
    Sequence<TypeRef> Cases,
    Location Location);