using DUnion;

namespace TestCases;

[DUnion]
public partial class Union
{
    [DUnionCase(IsCaseName = "MyIsCase1Method")]
    public delegate void Case1();

    [DUnionCase(IsCaseName = "MyIsCase2Method")]
    public delegate void Case2();
}