using DUnion;

namespace TestCases;

[DUnion]
public partial class Union
{
    public enum Case1
    {
        A,
        B,
        C
    }

    public enum Case2
    {
        A,
        B,
        C
    }

    [DUnionExclude]
    public class NotCase3
    {
    }
}