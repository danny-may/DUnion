using DUnion;

namespace TestCases;

[DUnion]
public partial struct Union
{
    [DUnionCase(IsCaseName = "MyIsCase1Method")]
    public record Case1
    {
    }

    [DUnionCase(IsCaseName = "MyIsCase2Method")]
    public record Case2
    {
    }
}