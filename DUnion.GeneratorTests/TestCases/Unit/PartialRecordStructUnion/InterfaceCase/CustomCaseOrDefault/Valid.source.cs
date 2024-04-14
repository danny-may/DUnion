using DUnion;

namespace TestCases;

[DUnion]
public partial record struct Union
{
    [DUnionCase(CaseOrDefaultName = "MyCase1OrDefaultMethod")]
    public interface ICase1
    {
    }

    [DUnionCase(CaseOrDefaultName = "MyCase2OrDefaultMethod")]
    public interface ICase2
    {
    }
}