using DUnion;

namespace Valid.NestedInInterface;

public partial interface IContainer 
{
    [DUnion]
    public readonly partial record struct Union 
    {
        public record struct Case();
    }
}