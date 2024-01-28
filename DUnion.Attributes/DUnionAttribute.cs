#if !DUNION_OMIT_ATTRIBUTES
#pragma warning disable

using System.Collections.Generic;

namespace DUnion
{
    /// <summary>
    /// A marker attribute to indicate that this type should have source generated to make it behave like a discriminated union type.
    /// Any externally visible types contained within the decorated type will be treated as potential values for the generated union.
    /// <br />
    /// The generated code will contain some auto-generated members: <c>Switch</c>, <c>Match</c>, <c>IsFoo</c>, <c>Discriminator</c> and <c>UnderlyingValue</c>
    /// </summary>
    [global::System.AttributeUsageAttribute(global::System.AttributeTargets.Class | global::System.AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public sealed class DUnionAttribute : global::System.Attribute
    {
        /// <summary>
        /// Sets the name of the unions discriminator value
        /// </summary>
        public string Discriminator { get; set; }

        /// <summary>
        /// Sets the template used for the IsX method
        /// </summary>
        public string IsCase { get; set; }

        /// <summary>
        /// Changes how the union is implemented
        /// </summary>
        public UnionKind Kind { get; set; }

        /// <summary>
        /// Sets the name of the unions Match method
        /// </summary>
        public string Match { get; set; }

        /// <summary>
        /// Sets the name of the unions Switch method
        /// </summary>
        public string Switch { get; set; }

        /// <summary>
        /// Sets the name of the unions underlying value
        /// </summary>
        public string UnderlyingValue { get; set; }
    }
}

#endif