using DUnion;

namespace TestCases;

[DUnion(ValueName = "This isnt a valid value")]
public partial record struct Union
{
    public record Case1
    {
    }

    public record Case2
    {
    }
}