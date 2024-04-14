using DUnion;

namespace TestCases;

[DUnion(MatchName = "This isnt a valid match method")]
public partial record struct Union
{
    public delegate void Case1();

    public delegate void Case2();
}