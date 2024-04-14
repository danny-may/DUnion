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

    public class MyClass
    {
        [DUnion]
        public object Union { get; set; }
    }
}