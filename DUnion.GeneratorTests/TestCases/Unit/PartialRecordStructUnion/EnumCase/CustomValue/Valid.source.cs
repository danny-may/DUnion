using DUnion;

namespace TestCases;

[DUnion(VallueName = "MyValue")]
public partial record struct Union
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