using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "This isnt a valid discriminator")]
public partial class Union
{
    public interface ICase1
    {
    }

    public interface ICase2
    {
    }
}