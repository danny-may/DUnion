using DUnion;

namespace SubTyped.Valid.NestedInInterface;

public partial interface IContainer 
{
    [DUnion(Kind = UnionKind.SubType)]
    public partial record class Union 
    {
        public partial record class Case();
    }
}