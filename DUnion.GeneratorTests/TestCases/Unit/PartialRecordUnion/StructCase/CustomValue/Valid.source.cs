using DUnion;

namespace TestCases;

[DUnion(VallueName = "MyValue")]
public partial record Union
{
    public struct Case1
    {
    }

    public struct Case2
    {
    }
}