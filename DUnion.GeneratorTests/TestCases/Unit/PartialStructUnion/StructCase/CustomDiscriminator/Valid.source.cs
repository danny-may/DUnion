using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "MyDiscriminator")]
public partial struct Union
{
    public struct Case1
    {
    }

    public struct Case2
    {
    }
}