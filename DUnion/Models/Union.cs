namespace DUnion.Models;

internal sealed record Union(TypeId Id, TypeDefinition Definition, UnionConfig Config, Sequence<UnionCase> Cases);