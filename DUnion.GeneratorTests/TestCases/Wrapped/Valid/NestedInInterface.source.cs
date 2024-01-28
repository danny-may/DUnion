using DUnion;

namespace Wrapped.Valid.NestedInInterface;

public partial interface IContainer 
{
    [DUnion]
    public readonly partial record struct Union 
    {
        public record struct Case();
    }
}