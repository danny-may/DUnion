using DUnion;

namespace TestCases;

[DUnion(MatchName = "MyMatchMethod")]
public partial struct Union
{
    public struct Case1
    {
    }

    public struct Case2
    {
    }
}