#if !DUNION_OMIT_ATTRIBUTES
#pragma warning disable

namespace DUnion
{
    /// <summary>
    /// Marks a type as being excluded from the containing union.
    /// </summary>
    [global::System.AttributeUsageAttribute(global::System.AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
    public sealed class DUnionGenericAttribute : global::System.Attribute
    {
        public string Name { get; }

        public DUnionGenericAttribute(string name)
        {
            Name = name;
        }
    }
}

#endif