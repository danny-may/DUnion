using DUnion;

namespace SubTyped.Valid.UnionClass;

[DUnion(Kind = UnionKind.SubType)]
public partial class Union 
{
    public partial class Case();
}