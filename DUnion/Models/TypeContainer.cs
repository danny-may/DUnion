using CA = Microsoft.CodeAnalysis;

namespace DUnion.Models;

internal sealed record TypeContainer(string Name, Sequence<TypeParameter> TypeParameters, bool IsRecord, CA.TypeKind Kind);