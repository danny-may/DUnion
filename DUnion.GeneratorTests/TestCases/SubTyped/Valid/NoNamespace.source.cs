using DUnion;

[DUnion(Kind = UnionKind.SubType)]
public partial record class NoNamespaceSubTyped 
{
    public partial record class Case();
}