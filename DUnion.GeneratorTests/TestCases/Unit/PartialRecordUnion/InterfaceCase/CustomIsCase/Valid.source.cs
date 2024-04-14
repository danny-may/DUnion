using DUnion;

namespace TestCases;

[DUnion]
public partial record Union
{
    [DUnionCase(IsCaseName = "MyIsCase1Method")]
    public interface ICase1
    {
    }

    [DUnionCase(IsCaseName = "MyIsCase2Method")]
    public interface ICase2
    {
    }
}