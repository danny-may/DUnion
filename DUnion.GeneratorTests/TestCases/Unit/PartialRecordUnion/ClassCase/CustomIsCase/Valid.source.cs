using DUnion;

namespace TestCases;

[DUnion]
public partial record Union
{
    [DUnionCase(IsCaseName = "MyIsCase1Method")]
    public class Case1
    {
    }

    [DUnionCase(IsCaseName = "MyIsCase2Method")]
    public class Case2
    {
    }
}