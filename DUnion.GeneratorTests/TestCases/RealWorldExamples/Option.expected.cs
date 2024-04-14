/* Diagnostics: 0 */

/* Sources: 1 */
/* File Path: DUnion\DUnion.SourceGenerator\TestCases.Option[T].g.cs */
// =============================================================
// This code was generated by the DUnion source generator.
// Changes to this file will be lost if the code is regenerated.
// =============================================================
#nullable enable
#pragma warning disable
namespace TestCases
{
    public readonly partial record struct Option<T> : System.IEquatable<TestCases.Option<T>>
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
        ///         <description><see cref="TestCases.Option.Some{T}" /></description>
        ///     </item>
        ///     <item>
        ///         <term><c>2</c></term>
        ///         <description><see cref="TestCases.Option.None" /></description>
        ///     </item>
        /// </list>
        /// </summary>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        [System.Diagnostics.DebuggerBrowsableAttribute(System.Diagnostics.DebuggerBrowsableState.Never)]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        private readonly System.Byte _discriminator;
    
        /// <summary>
        /// The underlying value that this union instance represents. Will be one of <see cref="TestCases.Option.Some{T}" />, <see cref="TestCases.Option.None" />.
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
        /// Determines whether the two <see cref="TestCases.Option{T}" /> instances are equal.
        /// </summary>
        /// <param name="left">The <see cref="TestCases.Option{T}" /> to compare to <paramref name="right"/>.</param>
        /// <param name="right">The <see cref="TestCases.Option{T}" /> to compare to <paramref name="left"/>.</param>
        /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise <c>false</c>.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public static System.Boolean Equals(TestCases.Option<T> left, TestCases.Option<T> right)
        {
            
            return left.Equals(right);
        }
    
        
    
        
        /// <summary>
        /// Creates a new instance of the <see cref="TestCases.Option{T}" /> class, using a <see cref="TestCases.Option.Some{T}" /> as its value.
        /// </summary>
        /// <param name="value">The underlying value the <see cref="TestCases.Option{T}" /> instance will wrap.</param>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public Option(TestCases.Option.Some<T> value)
        {
            this._discriminator = 1;
            this._value = value;
        }
        
        /// <summary>
        /// Determines if the current <see cref="TestCases.Option{T}" /> instance represents a <see cref="TestCases.Option.Some{T}" /> or not.
        /// </summary>
        /// <param name="value">The <see cref="TestCases.Option.Some{T}" /> value this wraps if this <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.Some{T}" />, otherwise the default value of <see cref="TestCases.Option.Some{T}" />.</param>
        /// <returns><c>true</c> if this <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.Some{T}" />; otherwise <c>false</c>.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public System.Boolean IsSome(out TestCases.Option.Some<T> value)
        {
            if (this._discriminator == 1)
            {
                value = ((TestCases.Option.Some<T>)this._value);
                return true;
            }
            else
            {
                value = default(TestCases.Option.Some<T>);
                return false;
            }
        }
        
        /// <summary>
        /// Returns the value this <see cref="TestCases.Option{T}" /> represents if it is a <see cref="TestCases.Option.Some{T}" />; otherwise the default value of <see cref="TestCases.Option.Some{T}" />.
        /// </summary>
        /// <returns>the value this <see cref="TestCases.Option{T}" /> represents if it is a <see cref="TestCases.Option.Some{T}" />; otherwise the default value of <see cref="TestCases.Option.Some{T}" />.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public TestCases.Option.Some<T> AsSomeOrDefault()
        {
            if (this._discriminator == 1)
            {
                return ((TestCases.Option.Some<T>)this._value);
            }
            else
            {
                return default(TestCases.Option.Some<T>);
            }
        }
        
        /// <summary>
        /// Returns the value this <see cref="TestCases.Option{T}" /> represents if it is a <see cref="TestCases.Option.Some{T}" />; otherwise <paramref name="default" />.
        /// </summary>
        /// <returns>the value this <see cref="TestCases.Option{T}" /> represents if it is a <see cref="TestCases.Option.Some{T}" />; otherwise <paramref name="default" />.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public TestCases.Option.Some<T> AsSomeOrDefault(TestCases.Option.Some<T> @default)
        {
            if (this._discriminator == 1)
            {
                return ((TestCases.Option.Some<T>)this._value);
            }
            else
            {
                return @default;
            }
        }
        
        /// <summary>
        /// Returns the value this <see cref="TestCases.Option{T}" /> represents if it is a <see cref="TestCases.Option.Some{T}" />; otherwise the result of invoking <paramref name="default" />.
        /// </summary>
        /// <returns>the value this <see cref="TestCases.Option{T}" /> represents if it is a <see cref="TestCases.Option.Some{T}" />; otherwise the result of invoking <paramref name="default" />.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public TestCases.Option.Some<T> AsSomeOrDefault(System.Func<TestCases.Option.Some<T>> @default)
        {
            if (this._discriminator == 1)
            {
                return ((TestCases.Option.Some<T>)this._value);
            }
            else
            {
                return @default();
            }
        }
        
        /// <summary>
        /// Creates a new instance of the <see cref="TestCases.Option{T}" /> class, using a <see cref="TestCases.Option.Some{T}" /> as its value.
        /// </summary>
        /// <param name="value">The underlying value the <see cref="TestCases.Option{T}" /> instance will wrap.</param>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public static implicit operator TestCases.Option<T>(TestCases.Option.Some<T> value)
        {
            return new TestCases.Option<T>(value);
        }
        
        /// <summary>
        /// Returns the value that <paramref name="value" /> represents if it is a <see cref="TestCases.Option.Some{T}" />
        /// </summary>
        /// <returns>the value that <paramref name="value" /> represents if it is a <see cref="TestCases.Option.Some{T}" />.</returns>
        /// <exception cref="System.InvalidCastException">Thrown when the value represented by <paramref name="value" /> is not a <see cref="TestCases.Option.Some{T}" />.</exception>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public static explicit operator TestCases.Option.Some<T>(TestCases.Option<T> value)
        {
            if (value._discriminator == 1)
            {
                return ((TestCases.Option.Some<T>)value._value);
            }
            else
            {
                throw new System.InvalidCastException();
            }
        }
        
        /// <summary>
        /// Creates a new instance of the <see cref="TestCases.Option{T}" /> class, using a <see cref="TestCases.Option.None" /> as its value.
        /// </summary>
        /// <param name="value">The underlying value the <see cref="TestCases.Option{T}" /> instance will wrap.</param>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public Option(TestCases.Option.None value)
        {
            this._discriminator = 2;
            this._value = value;
        }
        
        /// <summary>
        /// Determines if the current <see cref="TestCases.Option{T}" /> instance represents a <see cref="TestCases.Option.None" /> or not.
        /// </summary>
        /// <param name="value">The <see cref="TestCases.Option.None" /> value this wraps if this <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.None" />, otherwise the default value of <see cref="TestCases.Option.None" />.</param>
        /// <returns><c>true</c> if this <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.None" />; otherwise <c>false</c>.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public System.Boolean IsNone(out TestCases.Option.None value)
        {
            if (this._discriminator == 2)
            {
                value = ((TestCases.Option.None)this._value);
                return true;
            }
            else
            {
                value = default(TestCases.Option.None);
                return false;
            }
        }
        
        /// <summary>
        /// Returns the value this <see cref="TestCases.Option{T}" /> represents if it is a <see cref="TestCases.Option.None" />; otherwise the default value of <see cref="TestCases.Option.None" />.
        /// </summary>
        /// <returns>the value this <see cref="TestCases.Option{T}" /> represents if it is a <see cref="TestCases.Option.None" />; otherwise the default value of <see cref="TestCases.Option.None" />.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public TestCases.Option.None AsNoneOrDefault()
        {
            if (this._discriminator == 2)
            {
                return ((TestCases.Option.None)this._value);
            }
            else
            {
                return default(TestCases.Option.None);
            }
        }
        
        /// <summary>
        /// Returns the value this <see cref="TestCases.Option{T}" /> represents if it is a <see cref="TestCases.Option.None" />; otherwise <paramref name="default" />.
        /// </summary>
        /// <returns>the value this <see cref="TestCases.Option{T}" /> represents if it is a <see cref="TestCases.Option.None" />; otherwise <paramref name="default" />.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public TestCases.Option.None AsNoneOrDefault(TestCases.Option.None @default)
        {
            if (this._discriminator == 2)
            {
                return ((TestCases.Option.None)this._value);
            }
            else
            {
                return @default;
            }
        }
        
        /// <summary>
        /// Returns the value this <see cref="TestCases.Option{T}" /> represents if it is a <see cref="TestCases.Option.None" />; otherwise the result of invoking <paramref name="default" />.
        /// </summary>
        /// <returns>the value this <see cref="TestCases.Option{T}" /> represents if it is a <see cref="TestCases.Option.None" />; otherwise the result of invoking <paramref name="default" />.</returns>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public TestCases.Option.None AsNoneOrDefault(System.Func<TestCases.Option.None> @default)
        {
            if (this._discriminator == 2)
            {
                return ((TestCases.Option.None)this._value);
            }
            else
            {
                return @default();
            }
        }
        
        /// <summary>
        /// Creates a new instance of the <see cref="TestCases.Option{T}" /> class, using a <see cref="TestCases.Option.None" /> as its value.
        /// </summary>
        /// <param name="value">The underlying value the <see cref="TestCases.Option{T}" /> instance will wrap.</param>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public static implicit operator TestCases.Option<T>(TestCases.Option.None value)
        {
            return new TestCases.Option<T>(value);
        }
        
        /// <summary>
        /// Returns the value that <paramref name="value" /> represents if it is a <see cref="TestCases.Option.None" />
        /// </summary>
        /// <returns>the value that <paramref name="value" /> represents if it is a <see cref="TestCases.Option.None" />.</returns>
        /// <exception cref="System.InvalidCastException">Thrown when the value represented by <paramref name="value" /> is not a <see cref="TestCases.Option.None" />.</exception>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public static explicit operator TestCases.Option.None(TestCases.Option<T> value)
        {
            if (value._discriminator == 2)
            {
                return ((TestCases.Option.None)value._value);
            }
            else
            {
                throw new System.InvalidCastException();
            }
        }
    
        /// <summary>
        /// Invokes one of the delegates based on what type this <see cref="TestCases.Option{T}" /> represents.
        /// <list type="table">
        ///     <listheader>
        ///         <term>Delegate.</term>
        ///         <description>When it will be invoked.</description>
        ///     </listheader>
        ///     <item>
        ///         <term><paramref name="caseSome" /></term>
        ///         <description>Invoked when this <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.Some{T}" />.</description>
        ///     </item>
        ///     <item>
        ///         <term><paramref name="caseNone" /></term>
        ///         <description>Invoked when this <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.None" />.</description>
        ///     </item>
        ///     <item>
        ///         <term><paramref name="default" /></term>
        ///         <description>Invoked when the delegate that would have otherwise been invoked was null.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="default"></param>
        /// <param name="caseSome">The delegate to invoke when the <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.Some{T}" />.</param>
        /// <param name="caseNone">The delegate to invoke when the <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.None" />.</param>
        /// <exception cref="System.InvalidOperationException">Thrown when this <see cref="TestCases.Option{T}" /> is not a valid instance. This means that the <see cref="_discriminator" /> has been tampered with via reflection, or <see cref="TestCases.Option{T}" /> is a struct and this is the default value of <see cref="TestCases.Option{T}" />.</exception>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public void Switch
        (
            System.Action? @default,
            System.Action<TestCases.Option.Some<T>>? caseSome = null,
            System.Action<TestCases.Option.None>? caseNone = null
        )
        {
            switch(this._discriminator)
            {
                case 0:
                    throw new System.InvalidOperationException("Union is not initialized");
    
                case 1:
                    if (!System.Object.ReferenceEquals(caseSome, null))
                    {
                        caseSome.Invoke(((TestCases.Option.Some<T>)this._value));
                    }
                    else if (!System.Object.ReferenceEquals(@default, null))
                    {
                        @default.Invoke();
                    }
                    break;
                
                case 2:
                    if (!System.Object.ReferenceEquals(caseNone, null))
                    {
                        caseNone.Invoke(((TestCases.Option.None)this._value));
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
        /// Invokes one of the delegates based on what type this <see cref="TestCases.Option{T}" /> represents.
        /// <list type="table">
        ///     <listheader>
        ///         <term>Delegate.</term>
        ///         <description>When it will be invoked.</description>
        ///     </listheader>
        ///     <item>
        ///         <term><paramref name="caseSome" /></term>
        ///         <description>Invoked when this <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.Some{T}" />.</description>
        ///     </item>
        ///     <item>
        ///         <term><paramref name="caseNone" /></term>
        ///         <description>Invoked when this <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.None" />.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="caseSome">The delegate to invoke when the <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.Some{T}" />.</param>
        /// <param name="caseNone">The delegate to invoke when the <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.None" />.</param>
        /// <exception cref="System.InvalidOperationException">Thrown when this <see cref="TestCases.Option{T}" /> is not a valid instance. This means that the <see cref="_discriminator" /> has been tampered with via reflection, or <see cref="TestCases.Option{T}" /> is a struct and this is the default value of <see cref="TestCases.Option{T}" />.</exception>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public void Switch
        (
            System.Action<TestCases.Option.Some<T>>? caseSome,
            System.Action<TestCases.Option.None>? caseNone
        )
        {
            switch(this._discriminator)
            {
                case 0:
                    throw new System.InvalidOperationException("Union is not initialized");
    
                case 1:
                    if (!System.Object.ReferenceEquals(caseSome, null))
                    {
                        caseSome.Invoke(((TestCases.Option.Some<T>)this._value));
                    }
                    break;
                
                case 2:
                    if (!System.Object.ReferenceEquals(caseNone, null))
                    {
                        caseNone.Invoke(((TestCases.Option.None)this._value));
                    }
                    break;
                
                default:
                    throw new System.InvalidOperationException("Union is not valid");
            }
        }
        /// <summary>
        /// Invokes one of the delegates based on what type this <see cref="TestCases.Option{T}" /> represents and returns its result.
        /// <list type="table">
        ///     <listheader>
        ///         <term>Delegate.</term>
        ///         <description>When it will be invoked.</description>
        ///     </listheader>
        ///     <item>
        ///         <term><paramref name="caseSome" /></term>
        ///         <description>Invoked when this <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.Some{T}" />.</description>
        ///     </item>
        ///     <item>
        ///         <term><paramref name="caseNone" /></term>
        ///         <description>Invoked when this <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.None" />.</description>
        ///     </item>
        ///     <item>
        ///         <term><paramref name="default" /></term>
        ///         <description>Invoked when the delegate that would have otherwise been invoked was null.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="default"></param>
        /// <param name="caseSome">The delegate to invoke when the <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.Some{T}" />.</param>
        /// <param name="caseNone">The delegate to invoke when the <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.None" />.</param>
        /// <returns>the result of invoking the relevant delegate.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when this <see cref="TestCases.Option{T}" /> is not a valid instance. This means that the <see cref="_discriminator" /> has been tampered with via reflection, or <see cref="TestCases.Option{T}" /> is a struct and this is the default value of <see cref="TestCases.Option{T}" />.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when both the delegate that should have been invoked and <paramref name="default" /> are null.</exception>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public TMatchResult Match<TMatchResult>
        (
            System.Func<TMatchResult> @default,
            System.Func<TestCases.Option.Some<T>, TMatchResult>? caseSome = null,
            System.Func<TestCases.Option.None, TMatchResult>? caseNone = null
        )
        {
            switch(this._discriminator)
            {
                case 0:
                    throw new System.InvalidOperationException("Union is not initialized");
    
                case 1:
                    if (!System.Object.ReferenceEquals(caseSome, null))
                    {
                        return caseSome.Invoke(((TestCases.Option.Some<T>)this._value));
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
                    if (!System.Object.ReferenceEquals(caseNone, null))
                    {
                        return caseNone.Invoke(((TestCases.Option.None)this._value));
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
        /// Invokes one of the delegates based on what type this <see cref="TestCases.Option{T}" /> represents and returns its result.
        /// <list type="table">
        ///     <listheader>
        ///         <term>Delegate.</term>
        ///         <description>When it will be invoked.</description>
        ///     </listheader>
        ///     <item>
        ///         <term><paramref name="caseSome" /></term>
        ///         <description>Invoked when this <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.Some{T}" />.</description>
        ///     </item>
        ///     <item>
        ///         <term><paramref name="caseNone" /></term>
        ///         <description>Invoked when this <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.None" />.</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="caseSome">The delegate to invoke when the <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.Some{T}" />.</param>
        /// <param name="caseNone">The delegate to invoke when the <see cref="TestCases.Option{T}" /> represents a <see cref="TestCases.Option.None" />.</param>
        /// <returns>the result of invoking the relevant delegate.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when this <see cref="TestCases.Option{T}" /> is not a valid instance. This means that the <see cref="_discriminator" /> has been tampered with via reflection, or <see cref="TestCases.Option{T}" /> is a struct and this is the default value of <see cref="TestCases.Option{T}" />.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the delegate that should have been invoked is null.</exception>
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute]
        public TMatchResult Match<TMatchResult>
        (
            System.Func<TestCases.Option.Some<T>, TMatchResult> caseSome,
            System.Func<TestCases.Option.None, TMatchResult> caseNone
        )
        {
            switch(this._discriminator)
            {
                case 0:
                    throw new System.InvalidOperationException("Union is not initialized");
    
                case 1:
                    if (!System.Object.ReferenceEquals(caseSome, null))
                    {
                        return caseSome.Invoke(((TestCases.Option.Some<T>)this._value));
                    }
                    else
                    {
                        throw new System.ArgumentNullException(nameof(caseSome));
                    }
                
                case 2:
                    if (!System.Object.ReferenceEquals(caseNone, null))
                    {
                        return caseNone.Invoke(((TestCases.Option.None)this._value));
                    }
                    else
                    {
                        throw new System.ArgumentNullException(nameof(caseNone));
                    }
                
                default:
                    throw new System.InvalidOperationException("Union is not valid");
            }
        }
    }
}