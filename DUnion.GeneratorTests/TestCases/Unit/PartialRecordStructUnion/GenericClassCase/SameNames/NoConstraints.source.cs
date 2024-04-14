using DUnion;

namespace TestCases;

[DUnion]
public partial record struct Union
{
    public class Case1<T>
    {
    }

    public class Case2<T>
    {
    }
}