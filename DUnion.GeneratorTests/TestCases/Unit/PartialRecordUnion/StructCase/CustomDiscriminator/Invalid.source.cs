using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "This isnt a valid discriminator")]
public partial record Union
{
    public struct Case1
    {
    }

    public struct Case2
    {
    }
}