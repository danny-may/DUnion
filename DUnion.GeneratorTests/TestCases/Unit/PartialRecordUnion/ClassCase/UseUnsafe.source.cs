using DUnion;

namespace TestCases;

[DUnion(UseUnsafe = true)]
public partial record Union
{
    public class Case1
    {
    }

    public class Case2
    {
    }
}