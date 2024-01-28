using Microsoft.CodeAnalysis.Text;
using System;
using CA = Microsoft.CodeAnalysis;

namespace DUnion.Models;

internal readonly record struct Location(string FilePath, TextSpan TextSpan, LinePositionSpan LineSpan)
{
    public static implicit operator CA.Location(Location location)
    {
        return CA.Location.Create(location.FilePath, location.TextSpan, location.LineSpan);
    }

    public static implicit operator Location(CA.Location location)
    {
        if (location is not { SourceTree.FilePath: string filePath })
            throw new ArgumentException("Location must contain a source tree", nameof(location));

        return new(filePath, location.SourceSpan, location.GetLineSpan().Span);
    }
}