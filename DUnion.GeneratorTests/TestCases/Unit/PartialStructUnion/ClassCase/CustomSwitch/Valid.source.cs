using DUnion;

namespace TestCases;

[DUnion(SwitchName = "MySwitchMethod")]
public partial struct Union
{
    public class Case1
    {
    }

    public class Case2
    {
    }
}