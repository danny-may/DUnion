using Microsoft.CodeAnalysis;

namespace DUnion.Models;

internal sealed record TypeContainer(string Name, Sequence<TypeParameter> TypeParameters, bool IsRecord, TypeKind Kind);