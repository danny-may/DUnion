using DUnion;

namespace TestCases;

[DUnion]
public partial record Union
{
    [DUnionCase(CaseOrDefaultName = "This isnt a valid case or default name")]
    public delegate void Case1();

    [DUnionCase(CaseOrDefaultName = "This also isnt a valid case or default name")]
    public delegate void Case2();
}