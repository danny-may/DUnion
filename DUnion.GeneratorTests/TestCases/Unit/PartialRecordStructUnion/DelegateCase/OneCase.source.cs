using DUnion;

namespace TestCases;

[DUnion]
public partial record struct Union
{
    public delegate void Case1();
}