using DUnion;

namespace TestCases;

[DUnion(SwitchName = "This isnt a valid switch method")]
public partial struct Union
{
    public delegate void Case1();

    public delegate void Case2();
}