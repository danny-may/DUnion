using DUnion;

namespace TestCases;

[DUnion(MatchName = "MyMatchMethod")]
public partial struct Union
{
    public interface ICase1
    {
    }

    public interface ICase2
    {
    }
}