using DUnion;

namespace TestCases;

[DUnion(UseUnsafe = true)]
public partial class Union
{
    public record Case1
    {
    }

    public record Case2
    {
    }
}