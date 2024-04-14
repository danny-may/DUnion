using DUnion;

namespace TestCases;

[DUnion(UseUnsafe = true)]
public partial class Union
{
    public struct Case1
    {
    }

    public struct Case2
    {
    }
}