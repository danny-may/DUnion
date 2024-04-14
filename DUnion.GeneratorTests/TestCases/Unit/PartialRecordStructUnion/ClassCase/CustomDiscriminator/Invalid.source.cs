using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "This isnt a valid discriminator")]
public partial record struct Union
{
    public class Case1
    {
    }

    public class Case2
    {
    }
}