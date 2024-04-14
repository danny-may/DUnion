using DUnion;

namespace TestCases;

[DUnion]
public partial struct Union
{
    public class Case1<T> where T : struct
    {
    }

    public class Case2<T> where T : class
    {
    }
}