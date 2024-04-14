using DUnion;

namespace TestCases;

[DUnion(SwitchName = "MySwitchMethod")]
public partial struct Union
{
    public interface ICase1
    {
    }

    public interface ICase2
    {
    }
}