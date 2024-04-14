using DUnion;

namespace TestCases;

[DUnion]
public partial struct Union
{
    public delegate void Case1();

    public delegate void Case2();
}