using DUnion;

namespace TestCases;

[DUnion(ValueName = "This isnt a valid value")]
public partial class Union
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