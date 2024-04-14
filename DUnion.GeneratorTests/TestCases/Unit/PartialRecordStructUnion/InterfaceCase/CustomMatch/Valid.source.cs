using DUnion;

namespace TestCases;

[DUnion(MatchName = "MyMatchMethod")]
public partial record struct Union
{
    public interface ICase1
    {
    }

    public interface ICase2
    {
    }
}