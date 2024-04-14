using DUnion;

namespace TestCases;

[DUnion]
public partial record struct Union
{
    public enum Case1
    {
        A,
        B,
        C
    }
}