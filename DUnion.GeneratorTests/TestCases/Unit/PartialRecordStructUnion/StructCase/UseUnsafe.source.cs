using DUnion;

namespace TestCases;

[DUnion(UseUnsafe = true)]
public partial record struct Union
{
    public struct Case1
    {
    }

    public struct Case2
    {
    }
}