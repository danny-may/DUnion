using DUnion;

namespace TestCases;

[DUnion(SwitchName = "MySwitchMethod")]
public partial class Union
{
    public struct Case1
    {
    }

    public struct Case2
    {
    }
}