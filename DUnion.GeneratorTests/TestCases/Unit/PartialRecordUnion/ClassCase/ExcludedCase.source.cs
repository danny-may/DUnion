using DUnion;

namespace TestCases;

[DUnion]
public partial record Union
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