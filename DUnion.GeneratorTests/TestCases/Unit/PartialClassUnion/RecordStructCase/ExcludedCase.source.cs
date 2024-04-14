using DUnion;

namespace TestCases;

[DUnion]
public partial class Union
{
    public record struct Case1
    {
    }

    public record struct Case2
    {
    }

    [DUnionExclude]
    public class NotCase3
    {
    }
}