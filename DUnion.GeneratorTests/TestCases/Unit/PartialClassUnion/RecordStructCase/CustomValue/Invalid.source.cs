using DUnion;

namespace TestCases;

[DUnion(ValueName = "This isnt a valid value")]
public partial class Union
{
    public record struct Case1
    {
    }

    public record struct Case2
    {
    }
}