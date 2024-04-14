using DUnion;

namespace TestCases;

[DUnion]
public partial struct Union
{
    public class Case1
    {
    }

    public class Case2
    {
    }

    [DUnionExclude]
    public class NotCase3
    {
    }
}