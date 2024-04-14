using DUnion;

namespace TestCases;

[DUnion(VallueName = "MyValue")]
public partial struct Union
{
    public interface ICase1
    {
    }

    public interface ICase2
    {
    }
}