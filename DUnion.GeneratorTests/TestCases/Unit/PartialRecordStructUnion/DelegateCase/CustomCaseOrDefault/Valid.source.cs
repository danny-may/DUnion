using DUnion;

namespace TestCases;

[DUnion]
public partial record struct Union
{
    [DUnionCase(CaseOrDefaultName = "MyCase1OrDefaultMethod")]
    public delegate void Case1();

    [DUnionCase(CaseOrDefaultName = "MyCase2OrDefaultMethod")]
    public delegate void Case2();
}