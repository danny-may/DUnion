using DUnion;

namespace TestCases;

[DUnion]
public partial record Union
{
    [DUnionCase(IsCaseName = "MyIsCase1Method")]
    public record struct Case1
    {
    }

    [DUnionCase(IsCaseName = "MyIsCase2Method")]
    public record struct Case2
    {
    }
}