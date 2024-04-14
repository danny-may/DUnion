using DUnion;

namespace TestCases;

[DUnion]
public partial record struct Union
{
    public struct Case1
    {
    }

    public struct Case2
    {
    }

    [DUnionExclude]
    public class NotCase3
    {
    }
}