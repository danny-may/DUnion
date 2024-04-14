using DUnion;

namespace TestCases;

[DUnion(UseUnsafe = true)]
public partial record Union
{
    public delegate void Case1();

    public delegate void Case2();
}