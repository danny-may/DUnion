using DUnion;

namespace TestCases;

[DUnion(ValueName = "This isnt a valid value")]
public partial record struct Union
{
    public struct Case1
    {
    }

    public struct Case2
    {
    }
}