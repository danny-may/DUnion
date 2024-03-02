using DUnion;

namespace Valid.NestedInUnion;

[DUnion]
public partial record struct Union 
{
    [DUnion]
    public readonly partial record struct Inner 
    {
        public record struct Case();
    }
    public record struct Case();
}