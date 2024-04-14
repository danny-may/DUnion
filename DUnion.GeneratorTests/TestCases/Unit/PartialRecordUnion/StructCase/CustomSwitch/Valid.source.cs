using DUnion;

namespace TestCases;

[DUnion(SwitchName = "MySwitchMethod")]
public partial record Union
{
    public struct Case1
    {
    }

    public struct Case2
    {
    }
}