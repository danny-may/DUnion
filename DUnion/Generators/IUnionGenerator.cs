using DUnion.Models;
using System;
using CA = Microsoft.CodeAnalysis;

namespace DUnion.Generators;

internal interface IUnionGenerator : IEquatable<IUnionGenerator>
{
    Location Location { get; }

    void Execute(CA.SourceProductionContext context);
}