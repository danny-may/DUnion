using DUnion;

namespace TestCases;

[DUnion(UseUnsafe = true)]
public partial record Union
{
    public struct Case1
    {
    }

    public struct Case2
    {
    }
}