using DUnion;

namespace TestCases;

[DUnion(MatchName = "MyMatchMethod")]
public partial struct Union
{
    public class Case1
    {
    }

    public class Case2
    {
    }
}