using DUnion;

namespace TestCases;

[DUnion]
public partial record Union
{
    public delegate void Case1();
}