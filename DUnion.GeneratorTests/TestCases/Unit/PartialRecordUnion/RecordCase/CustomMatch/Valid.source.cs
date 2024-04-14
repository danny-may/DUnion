using DUnion;

namespace TestCases;

[DUnion(MatchName = "MyMatchMethod")]
public partial record Union
{
    public record Case1
    {
    }

    public record Case2
    {
    }
}