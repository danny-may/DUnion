using DUnion;

namespace TestCases;

[DUnion]
public partial struct Union
{
    [DUnionCase(CaseOrDefaultName = "MyCase1OrDefaultMethod")]
    public record struct Case1
    {
    }

    [DUnionCase(CaseOrDefaultName = "MyCase2OrDefaultMethod")]
    public record struct Case2
    {
    }
}