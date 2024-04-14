using DUnion;

namespace TestCases;

[DUnion(UseUnsafe = true)]
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