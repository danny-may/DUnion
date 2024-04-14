using DUnion;

namespace TestCases;

[DUnion(MatchName = "This isnt a valid match method")]
public partial record Union
{
    public record Case1
    {
    }

    public record Case2
    {
    }
}