using DUnion;

namespace TestCases;

[DUnion(SwitchName = "This isnt a valid switch method")]
public partial record Union
{
    public interface ICase1
    {
    }

    public interface ICase2
    {
    }
}