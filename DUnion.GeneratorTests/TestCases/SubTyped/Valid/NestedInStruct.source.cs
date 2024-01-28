using DUnion;

namespace SubTyped.Valid.NestedInStruct;

public partial struct Container 
{
    [DUnion(Kind = UnionKind.SubType)]
    public partial record class Union 
    {
        public partial record class Case();
    }
}