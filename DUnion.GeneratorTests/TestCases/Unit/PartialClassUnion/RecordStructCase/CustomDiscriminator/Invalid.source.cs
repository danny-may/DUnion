using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "This isnt a valid discriminator")]
public partial class Union
{
    public record struct Case1
    {
    }

    public record struct Case2
    {
    }
}