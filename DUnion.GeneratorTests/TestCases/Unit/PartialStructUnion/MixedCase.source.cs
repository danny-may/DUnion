using DUnion;

namespace TestCases;

[DUnion]
public partial struct Union
{
    public class Case1
    {
    }

    public delegate void Case2();

    public enum Case3
    {
        A,
        B,
        C
    }

    public interface Case4 
    {
    }

    public record Case5
    {
    }

    public record struct Case6
    {
    }

    public struct Case7
    {
    }
}