using DUnion;

namespace TestCases;

[DUnion]
public partial struct Union
{
    [DUnionCase(IsCaseName = "This isnt a valid is case name")]
    public record Case1
    {
    }

    [DUnionCase(IsCaseName = "This also isnt a valid is case name")]
    public record Case2
    {
    }
}