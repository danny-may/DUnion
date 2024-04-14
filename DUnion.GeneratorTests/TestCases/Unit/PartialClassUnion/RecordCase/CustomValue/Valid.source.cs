using DUnion;

namespace TestCases;

[DUnion(VallueName = "MyValue")]
public partial class Union
{
    public record Case1
    {
    }

    public record Case2
    {
    }
}