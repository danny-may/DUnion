using DUnion;

namespace TestCases;

[DUnion(UseUnsafe = true)]
public partial class Union
{
    public record struct Case1
    {
    }

    public record struct Case2
    {
    }
}