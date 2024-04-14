using DUnion;

namespace TestCases;

[DUnion(MatchName = "MyMatchMethod")]
public partial record Union
{
    public class Case1
    {
    }

    public class Case2
    {
    }
}