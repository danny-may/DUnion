// #DEFINE DUNION_OMIT_ATTRIBUTES
namespace DUnion 
{
    sealed class DUnionAttribute : Attribute
    {
    }
}


namespace TestCases
{
    using DUnion;

    [DUnion]
    public delegate void Union();
}