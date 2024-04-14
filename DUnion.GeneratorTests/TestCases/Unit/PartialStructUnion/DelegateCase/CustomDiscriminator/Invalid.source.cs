using DUnion;

namespace TestCases;

[DUnion(DiscriminatorName = "This isnt a valid discriminator")]
public partial struct Union
{
    public delegate void Case1();

    public delegate void Case2();
}