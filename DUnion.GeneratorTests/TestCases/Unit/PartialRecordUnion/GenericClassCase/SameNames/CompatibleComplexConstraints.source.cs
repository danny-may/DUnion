using DUnion;

namespace TestCases;

[DUnion]
public partial record Union
{
    public class Case1<T1, T2> 
        where T1 : System.Collections.Generic.IEnumerable<T2>
    {
    }

    public class Case2<T1, T2> 
        where T1 : System.Collections.Generic.IEnumerable<T2>
    {
    }
}