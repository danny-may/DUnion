using DUnion;

namespace TestCases;

[DUnion(VallueName = "MyValue")]
public partial struct Union
{
    public record Case1
    {
    }

    public record Case2
    {
    }
}