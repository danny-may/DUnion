using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "MyDiscriminator")]
public partial class Union
{
    public class Case1
    {
    }

    public class Case2
    {
    }
}