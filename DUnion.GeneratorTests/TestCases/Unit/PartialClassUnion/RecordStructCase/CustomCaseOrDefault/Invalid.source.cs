using DUnion;

namespace TestCases;

[DUnion]
public partial class Union
{
    [DUnionCase(CaseOrDefaultName = "This isnt a valid case or default name")]
    public record struct Case1
    {
    }

    [DUnionCase(CaseOrDefaultName = "This also isnt a valid case or default name")]
    public record struct Case2
    {
    }
}