using DUnion;

namespace TestCases;

[DUnion(SwitchName = "MySwitchMethod")]
public partial record struct Union
{
    public record Case1
    {
    }

    public record Case2
    {
    }
}