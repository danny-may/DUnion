using DUnion;

namespace TestCases;

[DUnion(MatchName = "MyMatchMethod")]
public partial class Union
{
    public delegate void Case1();

    public delegate void Case2();
}