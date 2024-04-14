using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "MyDiscriminator")]
public partial record struct Union
{
    public delegate void Case1();

    public delegate void Case2();
}