using DUnion;

namespace Valid.NestedInClass;

public partial class Container 
{
    [DUnion]
    public readonly partial record struct Union 
    {
        public record struct Case();
    }
}