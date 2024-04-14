using DUnion;

namespace TestCases;

[DUnion]
public partial record Union
{
    public record struct Case1
    {
    }

    public record struct Case2
    {
    }
}