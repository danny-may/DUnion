using DUnion;

namespace TestCases;

[DUnion(SwitchName = "This isnt a valid switch method")]
public partial record Union
{
    public class Case1
    {
    }

    public class Case2
    {
    }
}