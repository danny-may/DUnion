using DUnion;

namespace TestCases;

[DUnion(MatchName = "This isnt a valid match method")]
public partial class Union
{
    public record struct Case1
    {
    }

    public record struct Case2
    {
    }
}