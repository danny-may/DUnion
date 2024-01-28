using DUnion;

namespace Wrapped.Valid.UnionRecordClass;

[DUnion]
public partial record class Union 
{
    public record struct Case();
}