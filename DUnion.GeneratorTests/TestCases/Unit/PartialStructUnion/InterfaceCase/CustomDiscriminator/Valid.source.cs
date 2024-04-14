using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "MyDiscriminator")]
public partial struct Union
{
    public interface ICase1
    {
    }

    public interface ICase2
    {
    }
}