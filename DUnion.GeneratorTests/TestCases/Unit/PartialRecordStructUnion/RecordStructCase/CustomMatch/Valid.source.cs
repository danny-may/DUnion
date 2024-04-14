using DUnion;

namespace TestCases;

[DUnion(MatchName = "MyMatchMethod")]
public partial record struct Union
{
    public record struct Case1
    {
    }

    public record struct Case2
    {
    }
}