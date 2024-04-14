namespace DUnion.Models;

internal sealed record TypeId(string? Namespace, Sequence<TypeContainer> Containers, string Name, Sequence<TypeParameter> TypeParameters, Sequence<Location> Locations);