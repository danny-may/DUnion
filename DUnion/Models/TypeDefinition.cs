using Microsoft.CodeAnalysis;

namespace DUnion.Models;

internal sealed record TypeDefinition(Accessibility Accessibility, TypeKind Kind, bool IsRecord);