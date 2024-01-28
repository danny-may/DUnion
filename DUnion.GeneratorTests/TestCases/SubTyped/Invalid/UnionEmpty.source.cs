using DUnion;

namespace SubTyped.Invalid.UnionEmpty;

[DUnion(Kind = UnionKind.SubType)]
public partial record class Union 
{
}