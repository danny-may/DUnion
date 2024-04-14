using DUnion;

namespace TestCases;

[DUnion(MatchName = "MyMatchMethod")]
public partial class Union
{
    public struct Case1
    {
    }

    public struct Case2
    {
    }
}