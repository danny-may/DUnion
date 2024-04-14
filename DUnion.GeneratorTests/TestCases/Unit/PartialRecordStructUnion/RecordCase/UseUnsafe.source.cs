using DUnion;

namespace TestCases;

[DUnion(UseUnsafe = true)]
public partial record struct Union
{
    public record Case1
    {
    }

    public record Case2
    {
    }
}