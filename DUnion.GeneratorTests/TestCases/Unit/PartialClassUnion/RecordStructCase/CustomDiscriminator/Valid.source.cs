using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "MyDiscriminator")]
public partial class Union
{
    public record struct Case1
    {
    }

    public record struct Case2
    {
    }
}