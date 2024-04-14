using DUnion;

namespace TestCases;

[DUnion(UseUnsafe = true)]
public partial struct Union
{
    public record struct Case1
    {
    }

    public record struct Case2
    {
    }
}