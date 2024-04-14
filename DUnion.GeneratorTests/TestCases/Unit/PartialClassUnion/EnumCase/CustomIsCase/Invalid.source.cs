using DUnion;

namespace TestCases;

[DUnion]
public partial class Union
{
    [DUnionCase(IsCaseName = "This isnt a valid is case name")]
    public enum Case1
    {
        A,
        B,
        C
    }

    [DUnionCase(IsCaseName = "This also isnt a valid is case name")]
    public enum Case2
    {
        A,
        B,
        C
    }
}