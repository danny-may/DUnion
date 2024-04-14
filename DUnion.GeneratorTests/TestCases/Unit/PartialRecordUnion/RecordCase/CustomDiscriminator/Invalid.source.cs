using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "This isnt a valid discriminator")]
public partial record Union
{
    public record Case1
    {
    }

    public record Case2
    {
    }
}