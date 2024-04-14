using DUnion;

namespace TestCases;

[DUnion]
public partial record Union
{
    [DUnionCase(CaseOrDefaultName = "MyCase1OrDefaultMethod")]
    public struct Case1
    {
    }

    [DUnionCase(CaseOrDefaultName = "MyCase2OrDefaultMethod")]
    public struct Case2
    {
    }
}