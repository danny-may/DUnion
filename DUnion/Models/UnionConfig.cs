namespace DUnion.Models;

internal readonly record struct UnionConfig(string? Discriminator, string? UnderlyingValue, string? Switch, string? Match, string? IsCase);