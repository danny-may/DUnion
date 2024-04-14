using DUnion;

namespace TestCases;

[DUnion]
public partial record struct Union
{
    public ref struct Case1
    {
    }
    
    public ref struct Case2
    {
    }
}