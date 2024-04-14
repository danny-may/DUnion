using DUnion;

namespace TestCases;

[DUnion]
public partial record struct Union
{
    [DUnionCase(CaseOrDefaultName = "This isnt a valid case or default name")]
    public enum Case1
    {
        A,
        B,
        C
    }

    [DUnionCase(CaseOrDefaultName = "This also isnt a valid case or default name")]
    public enum Case2
    {
        A,
        B,
        C
    }
}