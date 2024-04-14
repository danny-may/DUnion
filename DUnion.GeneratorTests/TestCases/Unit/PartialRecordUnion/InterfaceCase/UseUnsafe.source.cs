using DUnion;

namespace TestCases;

[DUnion(UseUnsafe = true)]
public partial record Union
{
    public interface ICase1
    {
    }

    public interface ICase2
    {
    }
}