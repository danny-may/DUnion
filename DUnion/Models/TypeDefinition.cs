using CA = Microsoft.CodeAnalysis;

namespace DUnion.Models;

internal sealed record TypeDefinition(CA.Accessibility Accessibility, CA.TypeKind Kind, bool IsRecord);