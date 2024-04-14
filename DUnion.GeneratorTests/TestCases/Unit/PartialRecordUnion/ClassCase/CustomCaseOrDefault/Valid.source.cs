using DUnion;

namespace TestCases;

[DUnion]
public partial record Union
{
    [DUnionCase(CaseOrDefaultName = "MyCase1OrDefaultMethod")]
    public class Case1
    {
    }

    [DUnionCase(CaseOrDefaultName = "MyCase2OrDefaultMethod")]
    public class Case2
    {
    }
}