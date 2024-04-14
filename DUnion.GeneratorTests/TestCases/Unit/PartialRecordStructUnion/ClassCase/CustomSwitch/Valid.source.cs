using DUnion;

namespace TestCases;

[DUnion(SwitchName = "MySwitchMethod")]
public partial record struct Union
{
    public class Case1
    {
    }

    public class Case2
    {
    }
}