using DUnion;

namespace Valid.NestedInStruct;

public partial struct Container 
{
    [DUnion]
    public readonly partial record struct Union 
    {
        public record struct Case();
    }
}