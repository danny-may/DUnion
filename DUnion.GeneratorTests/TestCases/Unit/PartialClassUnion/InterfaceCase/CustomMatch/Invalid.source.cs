using DUnion;

namespace TestCases;

[DUnion(MatchName = "This isnt a valid match method")]
public partial class Union
{
    public interface ICase1
    {
    }

    public interface ICase2
    {
    }
}