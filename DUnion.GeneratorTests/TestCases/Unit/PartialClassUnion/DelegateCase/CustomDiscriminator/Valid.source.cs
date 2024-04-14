using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "MyDiscriminator")]
public partial class Union
{
    public delegate void Case1();

    public delegate void Case2();
}