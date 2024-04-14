namespace DUnion.Models;

internal sealed record UnionConfig(string DiscriminatorName, string ValueName, string SwitchName, string MatchName, bool UseUnsafe);