namespace DUnion.Models;

internal sealed record TypeParameter(string Name, Sequence<string> Constraints, Sequence<Location> Location);