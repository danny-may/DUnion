using DUnion;

namespace TestCases;

[DUnion]
public static class Union
{
    public class Case1<T>
    {
    }

    public class Case2<T>
    {
    }
}

public class Union<T>
{
    public Union() : this(new Union.Case1<T>())
    {
    }
}