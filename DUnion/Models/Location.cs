using Microsoft.CodeAnalysis.Text;
using System;
using System.Diagnostics.CodeAnalysis;
using CA = Microsoft.CodeAnalysis;

namespace DUnion.Models;

internal record class Location(string FilePath, TextSpan TextSpan, LinePositionSpan LineSpan)
{
    [return: NotNullIfNotNull(nameof(location))]
    public static implicit operator CA.Location?(Location? location)
    {
        if (location is null)
            return null;

        return CA.Location.Create(location.FilePath, location.TextSpan, location.LineSpan);
    }

    [return: NotNullIfNotNull(nameof(location))]
    public static implicit operator Location?(CA.Location? location)
    {
        if (location is null)
            return null;

        if (location is not { SourceTree.FilePath: string filePath })
            throw new ArgumentException("Location must contain a source tree", nameof(location));

        return new(filePath, location.SourceSpan, location.GetLineSpan().Span);
    }
}