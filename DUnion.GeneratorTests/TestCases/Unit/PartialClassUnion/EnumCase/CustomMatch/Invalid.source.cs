using DUnion;

namespace TestCases;

[DUnion(MatchName = "This isnt a valid match method")]
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