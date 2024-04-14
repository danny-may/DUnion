using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "MyDiscriminator")]
public partial struct Union
{
    public class Case1
    {
    }

    public class Case2
    {
    }
}