using DUnion;

namespace TestCases;

[DUnion(SwitchName = "This isnt a valid switch method")]
public partial class Union
{
    public record struct Case1
    {
    }

    public record struct Case2
    {
    }
}