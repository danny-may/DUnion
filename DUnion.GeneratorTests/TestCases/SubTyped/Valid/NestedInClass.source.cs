using DUnion;

namespace SubTyped.Valid.NestedInClass;

public partial class Container 
{
    [DUnion(Kind = UnionKind.SubType)]
    public partial record class Union 
    {
        public partial record class Case();
    }
}