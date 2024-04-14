using DUnion;

namespace TestCases;

[DUnion]
public partial record struct Union
{
    public class Case1<T1>
    {
    }

    public class Case2<T2>
    {
    }
}