using DUnion;

namespace TestCases;

[DUnion(SwitchName = "MySwitchMethod")]
public partial struct Union
{
    public enum Case1
    {
        A,
        B,
        C
    }

    public enum Case2
    {
        A,
        B,
        C
    }
}