using DUnion;

namespace TestCases;

[DUnion]
public partial struct Union
{
    public interface ICase1
    {
    }

    public interface ICase2
    {
    }

    [DUnionExclude]
    public class NotCase3
    {
    }
}