using DUnion;

namespace TestCases;

[DUnion(SwitchName = "This isnt a valid switch method")]
public partial struct Union
{
    public record Case1
    {
    }

    public record Case2
    {
    }
}