using DUnion;

namespace Wrapped.Valid.CaseInterface;

[DUnion]
public readonly partial record struct Union 
{
    public interface ICase { }
}