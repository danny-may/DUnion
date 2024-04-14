using DUnion;

namespace TestCases;

[DUnion(SwitchName = "MySwitchMethod")]
public partial record struct Union
{
    public delegate void Case1();

    public delegate void Case2();
}