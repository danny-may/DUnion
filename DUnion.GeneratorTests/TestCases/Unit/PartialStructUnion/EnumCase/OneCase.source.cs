using DUnion;

namespace TestCases;

[DUnion]
public partial struct Union
{
    public enum Case1
    {
        A,
        B,
        C
    }
}