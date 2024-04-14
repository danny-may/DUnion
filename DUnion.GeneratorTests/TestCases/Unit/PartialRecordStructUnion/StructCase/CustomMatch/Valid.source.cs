using DUnion;

namespace TestCases;

[DUnion(MatchName = "MyMatchMethod")]
public partial record struct Union
{
    public struct Case1
    {
    }

    public struct Case2
    {
    }
}