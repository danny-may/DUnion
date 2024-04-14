using DUnion;

namespace TestCases;

[DUnion(MatchName = "MyMatchMethod")]
public partial record Union
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