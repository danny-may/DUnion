using DUnion;

namespace TestCases;

[DUnion(VallueName = "MyValue")]
public partial record struct Union
{
    public delegate void Case1();

    public delegate void Case2();
}