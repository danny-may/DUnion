/* Diagnostics: 1 */
/*
(8,36): warning DYNUNION500: The 'SubTyped.Invalid.UnionInternalCaseProtected.Union.Ignored' case is less accessible than the 'SubTyped.Invalid.UnionInternalCaseProtected.Union' union which contains it, and so is not part of the union.
*/
/* Sources: 1 */
/* File Path: DUnion\DUnion.SourceGenerator\SubTyped.Invalid.UnionInternalCaseProtected.Union.DUnion.g.cs */
// =============================================================
// This code was generated by the DUnion source generator.
// Changes to this file will be lost if the code is regenerated.
// =============================================================
#pragma warning disable
#nullable enable


namespace SubTyped.Invalid.UnionInternalCaseProtected
{
    abstract partial record Union
    {
        /// <summary>A value used to discriminate what this instance represents.</summary>
        [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        [global::System.Diagnostics.DebuggerBrowsableAttribute(global::System.Diagnostics.DebuggerBrowsableState.Never)]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public abstract global::System.Byte @Discriminator { get; }
    
        public Union @UnderlyingValue => this;
    
        [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        private Union()
        {
        }
    
        /// <summary>
        /// Calls the appropriate delegate for the current type.
        /// <list type="bullet">
        /// <item>Calls <paramref name="caseCase"/> if this is a <see cref="Case"/></item>
        /// <item>Calls <paramref name="default"/> if the appropriate case* delegate wasnt provided.</item>
        /// </list>
        /// </summary>
        /// <param name="caseCase">Called if this is a <see cref="Case"/></param>
        /// <param name="default">Called if a delegate was not provided for this type</param>
        [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public void Switch(global::System.Action @default, global::System.Action<Case>? caseCase = null)
        {
            switch (this.@Discriminator)
            {
                case 0:
                    throw new global::System.InvalidOperationException("Union has not been initialized");
                case 1:
                    if (!global::System.Object.ReferenceEquals(caseCase, null))
                        caseCase((Case)this.@UnderlyingValue!);
                    else if (!global::System.Object.ReferenceEquals(@default, null))
                        @default();
                    else
                        throw new global::System.ArgumentNullException(nameof(@default));
                    break;
                default:
                    throw new global::System.InvalidOperationException($"Unsupported discriminator value {Discriminator}");
            }
        }
    
        /// <summary>
        /// Calls the appropriate delegate for the current type.
        /// <list type="bullet">
        /// <item>Calls <paramref name="caseCase"/> if this is a <see cref="Case"/></item>
        /// <item>Calls <paramref name="default"/> if the appropriate case* delegate wasnt provided.</item>
        /// </list>
        /// </summary>
        /// <param name="caseCase">Called if this is a <see cref="Case"/></param>
        /// <param name="default">Called if a delegate was not provided for this type</param>
        /// <returns>The value returned from the matched delegate</returns>
        [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public TMatchResult Match<TMatchResult>(global::System.Func<TMatchResult> @default, global::System.Func<Case, TMatchResult>? caseCase = null)
        {
            switch (this.@Discriminator)
            {
                case 0:
                    throw new global::System.InvalidOperationException("Union has not been initialized");
                case 1:
                    if (!global::System.Object.ReferenceEquals(caseCase, null))
                        return caseCase((Case)this.@UnderlyingValue!);
                    if (!global::System.Object.ReferenceEquals(@default, null))
                        return @default();
                    throw new global::System.ArgumentNullException(nameof(@default));
                default:
                    throw new global::System.InvalidOperationException($"Unsupported discriminator value {Discriminator}");
            }
        }
    
        /// <summary>
        /// Calls the appropriate delegate for the current type.
        /// <list type="bullet">
        /// <item>Calls <paramref name="caseCase"/> if this is a <see cref="Case"/></item>
        /// </list>
        /// </summary>
        /// <param name="caseCase">Called if this is a <see cref="Case"/></param>
        [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public void Switch(global::System.Action<Case> caseCase)
        {
            switch (this.@Discriminator)
            {
                case 0:
                    throw new global::System.InvalidOperationException("Union has not been initialized");
                case 1:
                    if (global::System.Object.ReferenceEquals(caseCase, null))
                        throw new global::System.ArgumentNullException(nameof(caseCase));
                    caseCase((Case)this.@UnderlyingValue!);
                    break;
                default:
                    throw new global::System.InvalidOperationException($"Unsupported discriminator value {Discriminator}");
            }
        }
    
        /// <summary>
        /// Calls the appropriate delegate for the current type.
        /// <list type="bullet">
        /// <item>Calls <paramref name="caseCase"/> if this is a <see cref="Case"/></item>
        /// </list>
        /// </summary>
        /// <param name="caseCase">Called if this is a <see cref="Case"/></param>
        /// <returns>The value returned from the matched delegate</returns>
        [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public TMatchResult Match<TMatchResult>(global::System.Func<Case, TMatchResult> caseCase)
        {
            switch (this.@Discriminator)
            {
                case 0:
                    throw new global::System.InvalidOperationException("Union has not been initialized");
                case 1:
                    if (global::System.Object.ReferenceEquals(caseCase, null))
                        throw new global::System.ArgumentNullException(nameof(caseCase));
                    return caseCase((Case)this.@UnderlyingValue!);
                default:
                    throw new global::System.InvalidOperationException($"Unsupported discriminator value {Discriminator}");
            }
        }
    
        
        /// <summary>
        /// If the current value is a <see cref="Case"/> then <paramref name="asCase"/> will be set to it and <c>true</c> will be returned,
        /// otherwise <paramref name="Case"/> will be set to <c>default(<see cref="Case"/>)</c> and <c>false</c> will be returned
        /// </summary>
        /// <param name="asCase">The current value if this is a <see cref="Case"/>, otherwise <c>default(<see cref="Case"/>)</c></param>
        /// <returns><c>true</c> if the current value is a <see cref="Case"/>, otherwise <c>false</c></returns>
        public bool IsCase([global::System.Diagnostics.CodeAnalysis.MaybeNullWhen(true)]out Case asCase)
        {
            if (this.@Discriminator != 1)
            {
                asCase = default(Case);
                return false;
            }
            asCase = (Case)this.@UnderlyingValue;
            return true;
        }
        
        
        partial record Case : Union
        {
            /// <summary>The discriminator value of <see cref="Case"/>.</summary>
            [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute]
            [global::System.Diagnostics.DebuggerBrowsableAttribute(global::System.Diagnostics.DebuggerBrowsableState.Never)]
            [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
            public sealed override global::System.Byte @Discriminator => 1;
        }
    }
}