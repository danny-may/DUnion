using DUnion;

namespace Valid.CaseInterface;

[DUnion]
public readonly partial record struct Union 
{
    public interface ICase { }
}