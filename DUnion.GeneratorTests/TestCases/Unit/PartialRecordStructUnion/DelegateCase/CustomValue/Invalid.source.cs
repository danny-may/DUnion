using DUnion;

namespace TestCases;

[DUnion(ValueName = "This isnt a valid value")]
public partial record struct Union
{
    public delegate void Case1();

    public delegate void Case2();
}