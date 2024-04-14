using DUnion;

namespace TestCases;

[DUnion(SwitchName = "This isnt a valid switch method")]
public partial record Union
{
    public struct Case1
    {
    }

    public struct Case2
    {
    }
}