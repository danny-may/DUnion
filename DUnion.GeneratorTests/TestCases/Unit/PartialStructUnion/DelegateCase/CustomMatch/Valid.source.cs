using DUnion;

namespace TestCases;

[DUnion(MatchName = "MyMatchMethod")]
public partial struct Union
{
    public delegate void Case1();

    public delegate void Case2();
}