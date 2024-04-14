using DUnion;

namespace TestCases;

[DUnion(SwitchName = "MySwitchMethod")]
public partial record Union
{
    public delegate void Case1();

    public delegate void Case2();
}