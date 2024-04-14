using DUnion;

namespace TestCases;

[DUnion(ValueName = "This isnt a valid value")]
public partial record Union
{
    public class Case1
    {
    }

    public class Case2
    {
    }
}