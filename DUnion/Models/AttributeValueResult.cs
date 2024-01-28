using Microsoft.CodeAnalysis;

namespace DUnion.Models;

internal readonly record struct AttributeValueResult(TypedConstant Value, SyntaxNode? Node);