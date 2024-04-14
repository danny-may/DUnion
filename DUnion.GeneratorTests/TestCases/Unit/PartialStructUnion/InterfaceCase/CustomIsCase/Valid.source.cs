using DUnion;

namespace TestCases;

[DUnion]
public partial struct Union
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