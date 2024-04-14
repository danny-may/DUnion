using DUnion;

namespace TestCases;

[DUnion(UseUnsafe = true)]
public partial struct Union
{
    public class Case1
    {
    }

    public class Case2
    {
    }
}