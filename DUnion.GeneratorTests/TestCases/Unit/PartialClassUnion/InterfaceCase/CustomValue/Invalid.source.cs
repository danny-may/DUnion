using DUnion;

namespace TestCases;

[DUnion(ValueName = "This isnt a valid value")]
public partial class Union
{
    public interface ICase1
    {
    }

    public interface ICase2
    {
    }
}