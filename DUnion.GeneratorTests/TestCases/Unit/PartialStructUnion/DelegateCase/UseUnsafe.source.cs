using DUnion;

namespace TestCases;

[DUnion(UseUnsafe = true)]
public partial struct Union
{
    public delegate void Case1();

    public delegate void Case2();
}