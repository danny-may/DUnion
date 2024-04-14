using DUnion;

namespace TestCases;

[DUnion(VallueName = "MyValue")]
public partial struct Union
{
    public record struct Case1
    {
    }

    public record struct Case2
    {
    }
}