using DUnion;

namespace TestCases;

[DUnion]
public partial struct Union
{
    [DUnionCase(IsCaseName = "MyIsCase1Method")]
    public struct Case1
    {
    }

    [DUnionCase(IsCaseName = "MyIsCase2Method")]
    public struct Case2
    {
    }
}