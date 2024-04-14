using DUnion;

namespace TestCases;

[DUnion]
public partial struct Union
{
    [DUnionCase(CaseOrDefaultName = "This isnt a valid case or default name")]
    public record Case1
    {
    }

    [DUnionCase(CaseOrDefaultName = "This also isnt a valid case or default name")]
    public record Case2
    {
    }
}