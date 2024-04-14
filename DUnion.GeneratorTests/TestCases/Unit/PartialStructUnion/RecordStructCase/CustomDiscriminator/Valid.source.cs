using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "MyDiscriminator")]
public partial struct Union
{
    public record struct Case1
    {
    }

    public record struct Case2
    {
    }
}