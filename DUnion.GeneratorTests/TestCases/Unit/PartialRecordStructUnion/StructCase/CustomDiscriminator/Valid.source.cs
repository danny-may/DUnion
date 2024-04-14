using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "MyDiscriminator")]
public partial record struct Union
{
    public struct Case1
    {
    }

    public struct Case2
    {
    }
}