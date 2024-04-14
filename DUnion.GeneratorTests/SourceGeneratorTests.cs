using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace DUnion.GeneratorTests;

public partial class SourceGeneratorTests
{
    private const string _expectedPostfix = ".expected.cs";
    private const string _testCasesPostfix = ".source.cs";
    private const string _testCasesPrefix = "DUnion.GeneratorTests.TestCases.";
    private static readonly Assembly _assembly = typeof(SourceGeneratorTests).Assembly;
    private readonly SourceGenerator _sut;

    public SourceGeneratorTests()
    {
        _sut = new();
    }

    public static TheoryData<string> GeneratesTheExpectedResult_TestCases()
    {
        var result = new TheoryData<string>();
        foreach (var resource in _assembly.GetManifestResourceNames())
        {
            if (resource.StartsWith(_testCasesPrefix) && resource.EndsWith(_testCasesPostfix))
            {
                result.Add(resource[_testCasesPrefix.Length..^_testCasesPostfix.Length]);
            }
        }
        return result;
    }

    [Theory]
    [MemberData(nameof(GeneratesTheExpectedResult_TestCases))]
    public void GeneratesTheExpectedResult(string testCase)
    {
        // arrange
        var source = ReadTestCase(testCase);
        var expected = ReadExpected(testCase) ?? "";

        // act
        var actual = RunSourceGenerator(source);

        // assert
        try
        {
            AssertNoDiff(actual, expected);
        }
        catch
        {
            try
            {
                var file = $"{testCase.Replace('.', '/')}{_expectedPostfix}";
                var dir = "Actuals/" + Path.GetDirectoryName(file);
                if (dir is not null && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                File.WriteAllText("Actuals/" + file, actual);
            }
            catch { }
            throw;
        }
    }

    private static void AssertNoDiff(string actual, string expected)
    {
        var diff = InlineDiffBuilder.Diff(expected, actual);
        var diffStr = new StringBuilder();
        foreach (var line in diff.Lines)
        {
            if (line.Type is ChangeType.Unchanged)
                continue;
            if (line.Position is { } position)
                diffStr.Append(position);
            diffStr.Append('\t');
            diffStr.Append(line.Type switch
            {
                ChangeType.Inserted => "+ ",
                ChangeType.Deleted => "- ",
                _ => "  ",
            });
            diffStr.AppendLine(line.Text);
        }
        if (diffStr.Length == 0)
            return;

        Assert.Fail("Expected the generated source(s) to match the expectation, but there were differences:\n" + diffStr);
    }

    [GeneratedRegex(@"(?<=^|\r?\n)// #DEFINE (.*?)(?:\r?\n|$)", RegexOptions.Compiled)]
    private static partial Regex DefineRegex();

    private static string? ReadExpected(string name)
    {
        using var stream = _assembly.GetManifestResourceStream($"{_testCasesPrefix}{name}{_expectedPostfix}");
        if (stream is null)
            return null;
        using var sr = new StreamReader(stream);
        return sr.ReadToEnd();
    }

    private static string ReadTestCase(string name)
    {
        using var stream = _assembly.GetManifestResourceStream($"{_testCasesPrefix}{name}{_testCasesPostfix}")!;
        using var sr = new StreamReader(stream);
        return sr.ReadToEnd();
    }

    private string RunSourceGenerator(string source)
    {
        var preprocessorSymbols = new List<string>();
        var regex = DefineRegex();
        source = regex.Replace(source, m =>
        {
            preprocessorSymbols.Add(m.Groups[1].Value);
            return "";
        });

        var tree = CSharpSyntaxTree.ParseText(source, options: new(preprocessorSymbols: preprocessorSymbols));
        var references = AppDomain.CurrentDomain.GetAssemblies()
            .Where(x => !x.IsDynamic && !string.IsNullOrWhiteSpace(x.Location))
            .Select(x => MetadataReference.CreateFromFile(x.Location));
        var compilation = CSharpCompilation.Create("DUnionTests", syntaxTrees: [tree], references: references);
        var driver = CSharpGeneratorDriver.Create(_sut);
        var result = driver.RunGenerators(compilation).GetRunResult();

        var generated = result.GeneratedTrees
            .Where(x => !x.FilePath.StartsWith("DUnion\\DUnion.SourceGenerator\\DUnion."));

        return $$"""
            /* Diagnostics: {{result.Diagnostics.Length}} */
            {{string.Join("\n==========\n", result.Diagnostics.Select(d => $"/*\n{d}\n*/"))}}
            /* Sources: {{generated.Count()}} */
            {{string.Join("\n==========\n", generated.Select(t => $"/* File Path: {t.FilePath} */\n{t}"))}}
            """;
    }
}