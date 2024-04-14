using DUnion;

namespace TestCases;

[DUnion(VallueName = "MyValue")]
public partial class Union
{
    public record struct Case1
    {
    }

    public record struct Case2
    {
    }
}