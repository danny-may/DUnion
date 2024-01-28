using System.Linq;
using CA = Microsoft.CodeAnalysis;

namespace DUnion.Models;

internal readonly record struct Diagnostic(CA.DiagnosticDescriptor Descriptor, Sequence<Location> Locations, Sequence<string> MessageArgs)
{
    public static implicit operator CA.Diagnostic(Diagnostic diagnostic)
    {
        return CA.Diagnostic.Create(
            descriptor: diagnostic.Descriptor,
            location: diagnostic.Locations.Select(l => (Location?)l).FirstOrDefault(),
            additionalLocations: diagnostic.Locations.Skip(1).Select(l => (CA.Location)l),
            messageArgs: diagnostic.MessageArgs.ToArray());
    }
}