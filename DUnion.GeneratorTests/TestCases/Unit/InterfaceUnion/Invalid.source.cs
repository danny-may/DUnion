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
    public interface Union
    {
        public class Case1
        {
        }

        public class Case2
        {
        }
    }
}