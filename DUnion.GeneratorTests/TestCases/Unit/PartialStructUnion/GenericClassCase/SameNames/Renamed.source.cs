using DUnion;

namespace TestCases;

[DUnion]
public partial struct Union
{
    public class Case1<[DUnionGeneric("T1")]T> where T : class
    {
    }

    public class Case2<[DUnionGeneric("T2")]T> where T : struct
    {
    }
}