using DUnion;

namespace TestCases;

[DUnion(MatchName = "This isnt a valid match method")]
public partial struct Union
{
    public struct Case1
    {
    }

    public struct Case2
    {
    }
}