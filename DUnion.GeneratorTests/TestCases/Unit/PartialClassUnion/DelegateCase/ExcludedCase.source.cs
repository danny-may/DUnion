using DUnion;

namespace TestCases;

[DUnion]
public partial class Union
{
    public delegate void Case1();

    public delegate void Case2();

    [DUnionExclude]
    public class NotCase3
    {
    }
}