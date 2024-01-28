#if !DUNION_OMIT_ATTRIBUTES
#pragma warning disable

namespace DUnion
{
    /// <summary>
    /// Marks a type as being excluded from the containing union.
    /// </summary>
    [global::System.AttributeUsageAttribute(global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public sealed class DUnionExcludeAttribute : global::System.Attribute
    {
    }
}

#endif