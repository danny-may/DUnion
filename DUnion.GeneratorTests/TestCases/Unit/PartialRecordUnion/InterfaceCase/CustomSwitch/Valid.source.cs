using DUnion;

namespace TestCases;

[DUnion(SwitchName = "MySwitchMethod")]
public partial record Union
{
    public interface ICase1
    {
    }

    public interface ICase2
    {
    }
}