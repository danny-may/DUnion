using DUnion;

namespace TestCases;

[DUnion(MatchName = "This isnt a valid match method")]
public partial record struct Union
{
    public class Case1
    {
    }

    public class Case2
    {
    }
}