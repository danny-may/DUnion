using DUnion;

namespace TestCases;

[DUnion(MatchName = "MyMatchMethod")]
public partial record struct Union
{
    public class Case1
    {
    }

    public class Case2
    {
    }
}