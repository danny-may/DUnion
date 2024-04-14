using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "MyDiscriminator")]
public partial record struct Union
{
    public record Case1
    {
    }

    public record Case2
    {
    }
}