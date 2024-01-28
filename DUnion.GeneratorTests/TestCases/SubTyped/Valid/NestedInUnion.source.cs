using DUnion;

namespace SubTyped.Valid.NestedInUnion;

[DUnion(Kind = UnionKind.SubType)]
public partial record class Union 
{
    [DUnion(Kind = UnionKind.SubType, Discriminator = "InnerDiscriminator")]
    public partial record class Inner 
    {
        public partial record class Case();
    }
    public partial record class Case();
}