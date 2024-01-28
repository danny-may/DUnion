using DUnion;

namespace Wrapped.Valid.CaseRecordStruct;

[DUnion]
public readonly partial record struct Union 
{
    public record struct Case();
}