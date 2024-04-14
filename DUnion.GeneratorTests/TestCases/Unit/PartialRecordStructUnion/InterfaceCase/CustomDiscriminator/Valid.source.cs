using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "MyDiscriminator")]
public partial record struct Union
{
    public interface ICase1
    {
    }

    public interface ICase2
    {
    }
}