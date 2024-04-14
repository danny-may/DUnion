using DUnion;

namespace TestCases;

[DUnion]
public partial record struct Union
{
    [DUnionCase(CaseOrDefaultName = "This isnt a valid case or default name")]
    public interface ICase1
    {
    }

    [DUnionCase(CaseOrDefaultName = "This also isnt a valid case or default name")]
    public interface ICase2
    {
    }
}