using DUnion;

namespace TestCases;

[DUnion(VallueName = "MyValue")]
public partial record struct Union
{
    public struct Case1
    {
    }

    public struct Case2
    {
    }
}