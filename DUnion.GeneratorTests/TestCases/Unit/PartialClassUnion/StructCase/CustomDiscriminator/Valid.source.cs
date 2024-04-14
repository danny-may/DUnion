using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "MyDiscriminator")]
public partial class Union
{
    public struct Case1
    {
    }

    public struct Case2
    {
    }
}