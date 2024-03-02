using DUnion;

namespace Valid.CaseRecordClass;

[DUnion]
public readonly partial record struct Union 
{
    public record class Case();
}