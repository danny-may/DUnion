using DUnion;

namespace TestCases;

[DUnion(MatchName = "MyMatchMethod")]
public partial record struct Union
{
    public record Case1
    {
    }

    public record Case2
    {
    }
}