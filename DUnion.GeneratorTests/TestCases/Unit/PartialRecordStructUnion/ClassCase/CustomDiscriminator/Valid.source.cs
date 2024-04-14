using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "MyDiscriminator")]
public partial record struct Union
{
    public class Case1
    {
    }

    public class Case2
    {
    }
}