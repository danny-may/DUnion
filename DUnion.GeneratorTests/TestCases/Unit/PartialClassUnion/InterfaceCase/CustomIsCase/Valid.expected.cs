/* Diagnostics: 0 */

/* Sources: 1 */
/* File Path: DUnion\DUnion.SourceGenerator\TestCases.Union.g.cs */
// =============================================================
// This code was generated by the DUnion source generator.
// Changes to this file will be lost if the code is regenerated.
// =============================================================
#nullable enable
#pragma warning disable
namespace TestCases
{
    public partial class Union : System.IEquatable<TestCases.Union>
    {
        /// <summary>
        /// A discriminator value which indicates what the type of <see cref="_value"/> is.
        /// <list type="table">
        ///     <listheader>
        ///         <term>Discriminator value.</term>
        ///         <description>The type that <see cref="_value"/> will contain.</description>
        ///     </listheader>
        ///     <item>
        ///         <term><c>1</c></term>
        ///         <description><see cref="TestCases.Union.ICase1" /></description>
        ///     </item>
        ///     <item>
        ///         <term><c>2</c></term>
        ///         <description><see cref="TestCases.Union.ICase2" /></description>
        ///     </item>
        /// </list>
        /// </summary>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        [System.Diagnostics.DebuggerBrowsableAttribute(System.Diagnostics.DebuggerBrowsableState.Never)]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        private readonly System.Byte _discriminator;
    
        /// <summary>
        /// The underlying value that this union instance represents. Will be one of <see cref="TestCases.Union.ICase1" />, <see cref="TestCases.Union.ICase2" />.
        /// </summary>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        [System.Diagnostics.DebuggerBrowsableAttribute(System.Diagnostics.DebuggerBrowsableState.Never)]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        private readonly System.Object? _value;
    
        /// <summary>
        /// Returns the string representation of the current value.
        /// </summary>
        /// <returns>the string representation of the current value.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public override System.String ToString()
        {
            return this._value?.ToString() ?? "";
        }
    
        /// <inheritdoc />
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public override System.Int32 GetHashCode()
        {
            return new System.ValueTuple<System.Byte, System.Object?>(this._discriminator, this._value).GetHashCode();
        }
    
        /// <summary>
        /// Determines whether the two <see cref="TestCases.Union" /> instances are equal.
        /// </summary>
        /// <param name="left">The <see cref="TestCases.Union" /> to compare to <paramref name="right"/>.</param>
        /// <param name="right">The <see cref="TestCases.Union" /> to compare to <paramref name="left"/>.</param>
        /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise <c>false</c>.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public static System.Boolean Equals(TestCases.Union left, TestCases.Union right)
        {
            if (System.Object.ReferenceEquals(left, null))
            {
                return System.Object.ReferenceEquals(right, null);
            }
            return left.Equals(right);
        }
    
        /// <summary>
        /// Determines whether the two <see cref="TestCases.Union" /> instances are equal.
        /// </summary>
        /// <param name="left">The <see cref="TestCases.Union" /> to compare to <paramref name="right"/>.</param>
        /// <param name="right">The <see cref="TestCases.Union" /> to compare to <paramref name="left"/>.</param>
        /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise <c>false</c>.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public static System.Boolean operator ==(TestCases.Union left, TestCases.Union right)
        {
            return Equals(left, right);
        }
        
        /// <summary>
        /// Determines whether the two <see cref="TestCases.Union" /> instances are not equal.
        /// </summary>
        /// <param name="left">The <see cref="TestCases.Union" /> to compare to <paramref name="right"/>.</param>
        /// <param name="right">The <see cref="TestCases.Union" /> to compare to <paramref name="left"/>.</param>
        /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are not equal; otherwise <c>false</c>.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public static System.Boolean operator !=(TestCases.Union left, TestCases.Union right)
        {
            return !Equals(left, right);
        }
        
        /// <summary>
        /// Determines whether the this <see cref="TestCases.Union" /> instance is equal to some other <see cref="System.Object" />.
        /// </summary>
        /// <param name="other">The value to compare this <see cref="TestCases.Union" /> to.</param>
        /// <returns><c>true</c> if this <see cref="TestCases.Union" /> and <paramref name="other"/> are equal; otherwise <c>false</c>.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public override System.Boolean Equals(System.Object? other)
        {
            if (other is TestCases.Union)
            {
                return this.Equals(((TestCases.Union)other));
            }
        
            switch(this._discriminator)
            {
                case 1:
                    return other is TestCases.Union.ICase1 && System.Object.Equals(this._value, other);
                
                case 2:
                    return other is TestCases.Union.ICase2 && System.Object.Equals(this._value, other);
                
                default:
                    return false;
            }
        }
        
        /// <summary>
        /// Determines whether the this <see cref="TestCases.Union" /> instance is equal to another <see cref="TestCases.Union" /> instance.
        /// </summary>
        /// <param name="other">The <see cref="TestCases.Union" /> to compare this <see cref="TestCases.Union" /> to.</param>
        /// <returns><c>true</c> if this <see cref="TestCases.Union" /> and <paramref name="other"/> are equal; otherwise <c>false</c>.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public System.Boolean Equals(TestCases.Union other)
        {
            if (System.Object.ReferenceEquals(other, null))
            {
                return false;
            }
            return this._discriminator == other._discriminator
                && System.Object.Equals(this._value, other._value);
        }
    
        
        /// <summary>
        /// Creates a new instance of the <see cref="TestCases.Union" /> class, using a <see cref="TestCases.Union.ICase1" /> as its value.
        /// </summary>
        /// <param name="value">The underlying value the <see cref="TestCases.Union" /> instance will wrap.</param>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public Union(TestCases.Union.ICase1 value)
        {
            this._discriminator = 1;
            this._value = value;
        }
        
        /// <summary>
        /// Determines if the current <see cref="TestCases.Union" /> instance represents a <see cref="TestCases.Union.ICase1" /> or not.
        /// </summary>
        /// <param name="value">The <see cref="TestCases.Union.ICase1" /> value this wraps if this <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase1" />, otherwise the default value of <see cref="TestCases.Union.ICase1" />.</param>
        /// <returns><c>true</c> if this <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase1" />; otherwise <c>false</c>.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public System.Boolean MyIsCase1Method(out TestCases.Union.ICase1 value)
        {
            if (this._discriminator == 1)
            {
                value = ((TestCases.Union.ICase1)this._value);
                return true;
            }
            else
            {
                value = default(TestCases.Union.ICase1);
                return false;
            }
        }
        
        /// <summary>
        /// Determines if the current <see cref="TestCases.Union" /> instance represents a <see cref="TestCases.Union.ICase1" /> or not.
        /// </summary>
        /// <returns><c>true</c> if this <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase1" />; otherwise <c>false</c>.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public System.Boolean MyIsCase1Method()
        {
            return this._discriminator == 1;
        }
        
        /// <summary>
        /// Returns the value this <see cref="TestCases.Union" /> represents if it is a <see cref="TestCases.Union.ICase1" />; otherwise the default value of <see cref="TestCases.Union.ICase1" />.
        /// </summary>
        /// <returns>the value this <see cref="TestCases.Union" /> represents if it is a <see cref="TestCases.Union.ICase1" />; otherwise the default value of <see cref="TestCases.Union.ICase1" />.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public TestCases.Union.ICase1? AsICase1OrDefault()
        {
            if (this._discriminator == 1)
            {
                return ((TestCases.Union.ICase1)this._value);
            }
            else
            {
                return default(TestCases.Union.ICase1);
            }
        }
        
        /// <summary>
        /// Returns the value this <see cref="TestCases.Union" /> represents if it is a <see cref="TestCases.Union.ICase1" />; otherwise <paramref name="default" />.
        /// </summary>
        /// <returns>the value this <see cref="TestCases.Union" /> represents if it is a <see cref="TestCases.Union.ICase1" />; otherwise <paramref name="default" />.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public TestCases.Union.ICase1 AsICase1OrDefault(TestCases.Union.ICase1 @default)
        {
            if (this._discriminator == 1)
            {
                return ((TestCases.Union.ICase1)this._value);
            }
            else
            {
                return @default;
            }
        }
        
        /// <summary>
        /// Returns the value this <see cref="TestCases.Union" /> represents if it is a <see cref="TestCases.Union.ICase1" />; otherwise the result of invoking <paramref name="default" />.
        /// </summary>
        /// <returns>the value this <see cref="TestCases.Union" /> represents if it is a <see cref="TestCases.Union.ICase1" />; otherwise the result of invoking <paramref name="default" />.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public TestCases.Union.ICase1 AsICase1OrDefault(System.Func<TestCases.Union.ICase1> @default)
        {
            if (this._discriminator == 1)
            {
                return ((TestCases.Union.ICase1)this._value);
            }
            else
            {
                return @default();
            }
        }
        
        
        
        /// <summary>
        /// Creates a new instance of the <see cref="TestCases.Union" /> class, using a <see cref="TestCases.Union.ICase2" /> as its value.
        /// </summary>
        /// <param name="value">The underlying value the <see cref="TestCases.Union" /> instance will wrap.</param>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public Union(TestCases.Union.ICase2 value)
        {
            this._discriminator = 2;
            this._value = value;
        }
        
        /// <summary>
        /// Determines if the current <see cref="TestCases.Union" /> instance represents a <see cref="TestCases.Union.ICase2" /> or not.
        /// </summary>
        /// <param name="value">The <see cref="TestCases.Union.ICase2" /> value this wraps if this <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase2" />, otherwise the default value of <see cref="TestCases.Union.ICase2" />.</param>
        /// <returns><c>true</c> if this <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase2" />; otherwise <c>false</c>.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public System.Boolean MyIsCase2Method(out TestCases.Union.ICase2 value)
        {
            if (this._discriminator == 2)
            {
                value = ((TestCases.Union.ICase2)this._value);
                return true;
            }
            else
            {
                value = default(TestCases.Union.ICase2);
                return false;
            }
        }
        
        /// <summary>
        /// Determines if the current <see cref="TestCases.Union" /> instance represents a <see cref="TestCases.Union.ICase2" /> or not.
        /// </summary>
        /// <returns><c>true</c> if this <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase2" />; otherwise <c>false</c>.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public System.Boolean MyIsCase2Method()
        {
            return this._discriminator == 2;
        }
        
        /// <summary>
        /// Returns the value this <see cref="TestCases.Union" /> represents if it is a <see cref="TestCases.Union.ICase2" />; otherwise the default value of <see cref="TestCases.Union.ICase2" />.
        /// </summary>
        /// <returns>the value this <see cref="TestCases.Union" /> represents if it is a <see cref="TestCases.Union.ICase2" />; otherwise the default value of <see cref="TestCases.Union.ICase2" />.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public TestCases.Union.ICase2? AsICase2OrDefault()
        {
            if (this._discriminator == 2)
            {
                return ((TestCases.Union.ICase2)this._value);
            }
            else
            {
                return default(TestCases.Union.ICase2);
            }
        }
        
        /// <summary>
        /// Returns the value this <see cref="TestCases.Union" /> represents if it is a <see cref="TestCases.Union.ICase2" />; otherwise <paramref name="default" />.
        /// </summary>
        /// <returns>the value this <see cref="TestCases.Union" /> represents if it is a <see cref="TestCases.Union.ICase2" />; otherwise <paramref name="default" />.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public TestCases.Union.ICase2 AsICase2OrDefault(TestCases.Union.ICase2 @default)
        {
            if (this._discriminator == 2)
            {
                return ((TestCases.Union.ICase2)this._value);
            }
            else
            {
                return @default;
            }
        }
        
        /// <summary>
        /// Returns the value this <see cref="TestCases.Union" /> represents if it is a <see cref="TestCases.Union.ICase2" />; otherwise the result of invoking <paramref name="default" />.
        /// </summary>
        /// <returns>the value this <see cref="TestCases.Union" /> represents if it is a <see cref="TestCases.Union.ICase2" />; otherwise the result of invoking <paramref name="default" />.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public TestCases.Union.ICase2 AsICase2OrDefault(System.Func<TestCases.Union.ICase2> @default)
        {
            if (this._discriminator == 2)
            {
                return ((TestCases.Union.ICase2)this._value);
            }
            else
            {
                return @default();
            }
        }
        
        
    
        /// <summary>
        /// Invokes one of the delegates based on what type this <see cref="TestCases.Union" /> represents.
        /// <list type="table">
        ///     <listheader>
        ///         <term>Delegate.</term>
        ///         <description>When it will be invoked.</description>
        ///     </listheader>
        ///     <item>
        ///         <term><paramref name="caseICase1" /></term>
        ///         <description>Invoked when this <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase1" />.</description>
        ///     </item>
        ///     <item>
        ///         <term><paramref name="caseICase2" /></term>
        ///         <description>Invoked when this <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase2" />.</description>
        ///     </item>
        ///     <item>
        ///         <term><paramref name="default" /></term>
        ///         <description>Invoked when the delegate that would have otherwise been invoked was null.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="default"></param>
        /// <param name="caseICase1">The delegate to invoke when the <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase1" />.</param>
        /// <param name="caseICase2">The delegate to invoke when the <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase2" />.</param>
        /// <exception cref="System.InvalidOperationException">Thrown when this <see cref="TestCases.Union" /> is not a valid instance. This means that the <see cref="_discriminator" /> has been tampered with via reflection, or <see cref="TestCases.Union" /> is a struct and this is the default value of <see cref="TestCases.Union" />.</exception>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public void Switch
        (
            System.Action? @default,
            System.Action<TestCases.Union.ICase1>? caseICase1 = null,
            System.Action<TestCases.Union.ICase2>? caseICase2 = null
        )
        {
            switch(this._discriminator)
            {
                case 0:
                    throw new System.InvalidOperationException("Union is not initialized.");
    
                case 1:
                    if (!System.Object.ReferenceEquals(caseICase1, null))
                    {
                        caseICase1.Invoke(((TestCases.Union.ICase1)this._value));
                    }
                    else if (!System.Object.ReferenceEquals(@default, null))
                    {
                        @default.Invoke();
                    }
                    break;
                
                case 2:
                    if (!System.Object.ReferenceEquals(caseICase2, null))
                    {
                        caseICase2.Invoke(((TestCases.Union.ICase2)this._value));
                    }
                    else if (!System.Object.ReferenceEquals(@default, null))
                    {
                        @default.Invoke();
                    }
                    break;
                
                default:
                    throw new System.InvalidOperationException("Union is not valid");
            }
        }
    
        /// <summary>
        /// Invokes one of the delegates based on what type this <see cref="TestCases.Union" /> represents.
        /// <list type="table">
        ///     <listheader>
        ///         <term>Delegate.</term>
        ///         <description>When it will be invoked.</description>
        ///     </listheader>
        ///     <item>
        ///         <term><paramref name="caseICase1" /></term>
        ///         <description>Invoked when this <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase1" />.</description>
        ///     </item>
        ///     <item>
        ///         <term><paramref name="caseICase2" /></term>
        ///         <description>Invoked when this <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase2" />.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="caseICase1">The delegate to invoke when the <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase1" />.</param>
        /// <param name="caseICase2">The delegate to invoke when the <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase2" />.</param>
        /// <exception cref="System.InvalidOperationException">Thrown when this <see cref="TestCases.Union" /> is not a valid instance. This means that the <see cref="_discriminator" /> has been tampered with via reflection, or <see cref="TestCases.Union" /> is a struct and this is the default value of <see cref="TestCases.Union" />.</exception>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public void Switch
        (
            System.Action<TestCases.Union.ICase1>? caseICase1,
            System.Action<TestCases.Union.ICase2>? caseICase2
        )
        {
            switch(this._discriminator)
            {
                case 0:
                    throw new System.InvalidOperationException("Union is not initialized");
    
                case 1:
                    if (!System.Object.ReferenceEquals(caseICase1, null))
                    {
                        caseICase1.Invoke(((TestCases.Union.ICase1)this._value));
                    }
                    break;
                
                case 2:
                    if (!System.Object.ReferenceEquals(caseICase2, null))
                    {
                        caseICase2.Invoke(((TestCases.Union.ICase2)this._value));
                    }
                    break;
                
                default:
                    throw new System.InvalidOperationException("Union is not valid");
            }
        }
        /// <summary>
        /// Invokes one of the delegates based on what type this <see cref="TestCases.Union" /> represents and returns its result.
        /// <list type="table">
        ///     <listheader>
        ///         <term>Delegate.</term>
        ///         <description>When it will be invoked.</description>
        ///     </listheader>
        ///     <item>
        ///         <term><paramref name="caseICase1" /></term>
        ///         <description>Invoked when this <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase1" />.</description>
        ///     </item>
        ///     <item>
        ///         <term><paramref name="caseICase2" /></term>
        ///         <description>Invoked when this <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase2" />.</description>
        ///     </item>
        ///     <item>
        ///         <term><paramref name="default" /></term>
        ///         <description>Invoked when the delegate that would have otherwise been invoked was null.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="default"></param>
        /// <param name="caseICase1">The delegate to invoke when the <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase1" />.</param>
        /// <param name="caseICase2">The delegate to invoke when the <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase2" />.</param>
        /// <returns>the result of invoking the relevant delegate.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when this <see cref="TestCases.Union" /> is not a valid instance. This means that the <see cref="_discriminator" /> has been tampered with via reflection, or <see cref="TestCases.Union" /> is a struct and this is the default value of <see cref="TestCases.Union" />.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when both the delegate that should have been invoked and <paramref name="default" /> are null.</exception>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public TMatchResult Match<TMatchResult>
        (
            System.Func<TMatchResult> @default,
            System.Func<TestCases.Union.ICase1, TMatchResult>? caseICase1 = null,
            System.Func<TestCases.Union.ICase2, TMatchResult>? caseICase2 = null
        )
        {
            switch(this._discriminator)
            {
                case 0:
                    throw new System.InvalidOperationException("Union is not initialized");
    
                case 1:
                    if (!System.Object.ReferenceEquals(caseICase1, null))
                    {
                        return caseICase1.Invoke(((TestCases.Union.ICase1)this._value));
                    }
                    else if (!System.Object.ReferenceEquals(@default, null))
                    {
                        return @default.Invoke();
                    }
                    else
                    {
                        throw new System.ArgumentNullException(nameof(@default));
                    }
                
                case 2:
                    if (!System.Object.ReferenceEquals(caseICase2, null))
                    {
                        return caseICase2.Invoke(((TestCases.Union.ICase2)this._value));
                    }
                    else if (!System.Object.ReferenceEquals(@default, null))
                    {
                        return @default.Invoke();
                    }
                    else
                    {
                        throw new System.ArgumentNullException(nameof(@default));
                    }
                
                default:
                    throw new System.InvalidOperationException("Union is not valid");
            }
        }
        /// <summary>
        /// Invokes one of the delegates based on what type this <see cref="TestCases.Union" /> represents and returns its result.
        /// <list type="table">
        ///     <listheader>
        ///         <term>Delegate.</term>
        ///         <description>When it will be invoked.</description>
        ///     </listheader>
        ///     <item>
        ///         <term><paramref name="caseICase1" /></term>
        ///         <description>Invoked when this <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase1" />.</description>
        ///     </item>
        ///     <item>
        ///         <term><paramref name="caseICase2" /></term>
        ///         <description>Invoked when this <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase2" />.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="caseICase1">The delegate to invoke when the <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase1" />.</param>
        /// <param name="caseICase2">The delegate to invoke when the <see cref="TestCases.Union" /> represents a <see cref="TestCases.Union.ICase2" />.</param>
        /// <returns>the result of invoking the relevant delegate.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when this <see cref="TestCases.Union" /> is not a valid instance. This means that the <see cref="_discriminator" /> has been tampered with via reflection, or <see cref="TestCases.Union" /> is a struct and this is the default value of <see cref="TestCases.Union" />.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the delegate that should have been invoked is null.</exception>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public TMatchResult Match<TMatchResult>
        (
            System.Func<TestCases.Union.ICase1, TMatchResult> caseICase1,
            System.Func<TestCases.Union.ICase2, TMatchResult> caseICase2
        )
        {
            switch(this._discriminator)
            {
                case 0:
                    throw new System.InvalidOperationException("Union is not initialized");
    
                case 1:
                    if (!System.Object.ReferenceEquals(caseICase1, null))
                    {
                        return caseICase1.Invoke(((TestCases.Union.ICase1)this._value));
                    }
                    else
                    {
                        throw new System.ArgumentNullException(nameof(caseICase1));
                    }
                
                case 2:
                    if (!System.Object.ReferenceEquals(caseICase2, null))
                    {
                        return caseICase2.Invoke(((TestCases.Union.ICase2)this._value));
                    }
                    else
                    {
                        throw new System.ArgumentNullException(nameof(caseICase2));
                    }
                
                default:
                    throw new System.InvalidOperationException("Union is not valid");
            }
        }
    }
}