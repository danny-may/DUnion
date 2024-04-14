using DUnion;

namespace TestCases;

[DUnion(VallueName = "MyValue")]
public partial record Union
{
    public class Case1
    {
    }

    public class Case2
    {
    }
}