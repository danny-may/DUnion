using DUnion;

namespace TestCases;

[DUnion(SwitchName = "This isnt a valid switch method")]
public partial class Union
{
    public class Case1
    {
    }

    public class Case2
    {
    }
}