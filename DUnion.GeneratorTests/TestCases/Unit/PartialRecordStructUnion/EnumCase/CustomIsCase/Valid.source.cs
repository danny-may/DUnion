using DUnion;

namespace TestCases;

[DUnion]
public partial record struct Union
{
    [DUnionCase(IsCaseName = "MyIsCase1Method")]
    public enum Case1
    {
        A,
        B,
        C
    }

    [DUnionCase(IsCaseName = "MyIsCase2Method")]
    public enum Case2
    {
        A,
        B,
        C
    }
}