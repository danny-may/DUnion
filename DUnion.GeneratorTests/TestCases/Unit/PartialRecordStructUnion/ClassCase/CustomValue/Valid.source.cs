using DUnion;

namespace TestCases;

[DUnion(VallueName = "MyValue")]
public partial record struct Union
{
    public class Case1
    {
    }

    public class Case2
    {
    }
}