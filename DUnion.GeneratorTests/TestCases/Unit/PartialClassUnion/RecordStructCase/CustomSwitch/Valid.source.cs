using DUnion;

namespace TestCases;

[DUnion(SwitchName = "MySwitchMethod")]
public partial class Union
{
    public record struct Case1
    {
    }

    public record struct Case2
    {
    }
}