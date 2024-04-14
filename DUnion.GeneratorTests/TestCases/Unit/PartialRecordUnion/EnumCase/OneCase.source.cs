using DUnion;

namespace TestCases;

[DUnion]
public partial record Union
{
    public enum Case1
    {
        A,
        B,
        C
    }
}