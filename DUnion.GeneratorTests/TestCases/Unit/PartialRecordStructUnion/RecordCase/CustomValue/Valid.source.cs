using DUnion;

namespace TestCases;

[DUnion(VallueName = "MyValue")]
public partial record struct Union
{
    public record Case1
    {
    }

    public record Case2
    {
    }
}