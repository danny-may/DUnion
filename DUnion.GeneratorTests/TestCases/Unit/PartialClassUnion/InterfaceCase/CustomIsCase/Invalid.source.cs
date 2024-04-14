using DUnion;

namespace TestCases;

[DUnion]
public partial class Union
{
    [DUnionCase(IsCaseName = "This isnt a valid is case name")]
    public interface ICase1
    {
    }

    [DUnionCase(IsCaseName = "This also isnt a valid is case name")]
    public interface ICase2
    {
    }
}