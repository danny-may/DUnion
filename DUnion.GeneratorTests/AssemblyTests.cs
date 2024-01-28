using FluentAssertions;
using Microsoft.CodeAnalysis;

namespace DUnion.GeneratorTests;

public static class AssemblyTests
{
    [Fact]
    public static void ShouldNotExposeAnyPublicMembers()
    {
        // arrange
        var assembly = typeof(SourceGenerator).Assembly;

        // act
        var exposedTypes = assembly.GetTypes().Where(t => t.IsPublic);

        // assert
        exposedTypes.Should().BeEmpty();
    }
}
