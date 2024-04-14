using DUnion;

namespace TestCases;

[DUnion(SwitchName = "MySwitchMethod")]
public partial struct Union
{
    public record Case1
    {
    }

    public record Case2
    {
    }
}