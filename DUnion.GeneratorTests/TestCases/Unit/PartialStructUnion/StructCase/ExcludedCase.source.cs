using DUnion;

namespace TestCases;

[DUnion]
public partial struct Union
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