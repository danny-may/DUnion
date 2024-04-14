using DUnion;

namespace TestCases;

[DUnion]
public partial struct Union
{
    public record Case1
    {
    }

    public record Case2
    {
    }

    [DUnionExclude]
    public class NotCase3
    {
    }
}