using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "MyDiscriminator")]
public partial struct Union
{
    public delegate void Case1();

    public delegate void Case2();
}