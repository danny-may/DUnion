using DUnion;

namespace TestCases;

[DUnion(SwitchName = "MySwitchMethod")]
public partial class Union
{
    public delegate void Case1();

    public delegate void Case2();
}