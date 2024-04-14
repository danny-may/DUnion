using DUnion;

namespace TestCases;

[DUnion]
public partial class Union
{
    [DUnionCase(CaseOrDefaultName = "MyCase1OrDefaultMethod")]
    public enum Case1
    {
        A,
        B,
        C
    }

    [DUnionCase(CaseOrDefaultName = "MyCase2OrDefaultMethod")]
    public enum Case2
    {
        A,
        B,
        C
    }
}