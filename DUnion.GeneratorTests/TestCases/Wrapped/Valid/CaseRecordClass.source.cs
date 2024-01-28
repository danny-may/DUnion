using DUnion;

namespace Wrapped.Valid.CaseRecordClass;

[DUnion]
public readonly partial record struct Union 
{
    public record class Case();
}