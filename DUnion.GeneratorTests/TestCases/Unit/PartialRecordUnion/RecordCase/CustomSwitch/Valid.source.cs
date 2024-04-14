using DUnion;

namespace TestCases;

[DUnion(SwitchName = "MySwitchMethod")]
public partial record Union
{
    public record Case1
    {
    }

    public record Case2
    {
    }
}