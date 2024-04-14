using DUnion;

namespace TestCases;

[DUnion(UseUnsafe = true)]
public partial class Union
{
    public interface ICase1
    {
    }

    public interface ICase2
    {
    }
}