using DUnion;

namespace TestCases;

[DUnion(VallueName = "MyValue")]
public partial class Union
{
    public struct Case1
    {
    }

    public struct Case2
    {
    }
}