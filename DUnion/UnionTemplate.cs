using DUnion.Models;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using CA = Microsoft.CodeAnalysis;

namespace DUnion;

internal static class UnionTemplate
{
    private static readonly string _action = typeof(Action).FullName;
    private static readonly string _argumentNullException = typeof(ArgumentNullException).FullName;
    private static readonly string _bool = typeof(bool).FullName;
    private static readonly string _byte = typeof(byte).FullName;
    private static readonly string _compilerGenerated = typeof(CompilerGeneratedAttribute).FullName;
    private static readonly string _debuggerBrowsable = typeof(DebuggerBrowsableAttribute).FullName;
    private static readonly string _debuggerBrowsableState = typeof(DebuggerBrowsableState).FullName;
    private static readonly string _editorBrowsable = typeof(EditorBrowsableAttribute).FullName;
    private static readonly string _editorBrowsableState = typeof(EditorBrowsableState).FullName;
    private static readonly string _func = typeof(Func<>).FullName[..^2];
    private static readonly string _iEquatable = typeof(IEquatable<>).FullName[..^2];
    private static readonly string _int = typeof(int).FullName;
    private static readonly string _invalidCastException = typeof(InvalidCastException).FullName;
    private static readonly string _invalidOperationException = typeof(InvalidOperationException).FullName;
    private static readonly string _object = typeof(object).FullName;
    private static readonly string _string = typeof(string).FullName;
    private static readonly string _uint = typeof(uint).FullName;
    private static readonly string _unsafe = typeof(Unsafe).FullName;
    private static readonly string _ushort = typeof(ushort).FullName;
    private static readonly string _valueTuple = typeof(ValueTuple<>).FullName[..^2];

    public static string Render(Union model)
    {
        var result = new UnionRenderContext(model).ToString();

        foreach (var container in model.Id.Containers.Reverse())
        {
            var containerTParams = Helpers.Render(container.TypeParameters);
            var @struct = GetKindSource(container);

            result = $$"""
                partial {{@struct}} {{container.Name}}{{containerTParams}}
                {
                    {{result.Indent(4)}}
                }
                """;
        }

        if (model.Id.Namespace is { Length: > 0 } ns)
        {
            result = $$"""
                namespace {{ns}}
                {
                    {{result.Indent(4)}}
                }
                """;
        }

        return $$"""
            #nullable enable
            #pragma warning disable
            {{result}}
            """;
    }

    private static string GetKindSource(TypeContainer container)
    {
        return container switch
        {
            { Kind: CA.TypeKind.Struct, IsRecord: true } => "record struct",
            { Kind: CA.TypeKind.Struct, IsRecord: false } => "struct",
            { Kind: CA.TypeKind.Class, IsRecord: true } => "record class",
            { Kind: CA.TypeKind.Class, IsRecord: false } => "class",
            { Kind: CA.TypeKind.Interface } => "interface",
            _ => throw new NotSupportedException($"Unsupported container type {container.Kind}")
        };
    }

    private static string Indent(this string target, int count, char spacer = ' ')
    {
        var indent = new string(spacer, count);
        var lines = target.Split('\n');

        return string.Join("\n", [lines[0], .. lines.Skip(1).Select(l => indent + l)]);
    }

    private sealed class UnionRenderContext
    {
        private readonly bool _useUnsafe;

        public bool IsRecord { get; }

        public CA.TypeKind Kind { get; }

        public string Name { get; }

        private IEnumerable<UnionCaseRenderContext> Cases { get; }

        private string Class { get; }

        private string Discriminator { get; }

        private string DocSee { get; }

        private bool IsNonNull { get; }

        private string Match { get; }

        private string Switch { get; }

        private string TConststraints { get; }

        private string TDiscriminator { get; }

        private string TMatchResult { get; }

        private string TParams { get; }

        private string TUnion { get; }

        private string Value { get; }

        public UnionRenderContext(Union model)
        {
            Class = GetKindSource(model.Definition);
            TParams = Helpers.Render(model.Id.TypeParameters);
            TConststraints = RenderTypeConstraints(model.Id.TypeParameters);
            TUnion = Helpers.Render(model.Id);
            TDiscriminator = ComputeDiscriminatorType(model.Cases);
            Discriminator = model.Config.DiscriminatorName;
            Value = model.Config.ValueName;
            Switch = model.Config.SwitchName;
            Match = model.Config.MatchName;
            Name = model.Id.Name;
            Kind = model.Definition.Kind;
            IsRecord = model.Definition.IsRecord;
            IsNonNull = model.Definition.Kind is CA.TypeKind.Struct or CA.TypeKind.Enum;
            TMatchResult = ComputeMatchResultType(model);
            uint i = 1;
            Cases = model.Cases.Select(c => new UnionCaseRenderContext(this, c, i++)).ToArray();
            DocSee = Helpers.RenderDocSee(model.Id);

            _useUnsafe = model.Config.UseUnsafe;
        }

        public override string ToString()
        {
            return $$"""
                {{Class}} {{Name}}{{TParams}} : {{_iEquatable}}<{{TUnion}}>{{TConststraints}}
                {
                    /// <summary>
                    /// A discriminator value which indicates what the type of <see cref="{{Value}}"/> is.
                    /// <list type="table">
                    ///     <listheader>
                    ///         <term>Discriminator value.</term>
                    ///         <description>The type that <see cref="{{Value}}"/> will contain.</description>
                    ///     </listheader>
                    {{string.Join("\r\n", Cases.Select(c => $$"""
                    ///     <item>
                    ///         <term><c>{{c.Id}}</c></term>
                    ///         <description>{{c.DocSee}}</description>
                    ///     </item>
                    """)).Indent(4)}}
                    /// </list>
                    /// </summary>
                    [{{_compilerGenerated}}]
                    [{{_debuggerBrowsable}}({{_debuggerBrowsableState}}.Never)]
                    [{{_editorBrowsable}}({{_editorBrowsableState}}.Never)]
                    private readonly {{TDiscriminator}} {{Discriminator}};

                    /// <summary>
                    /// The underlying value that this union instance represents. Will be one of {{string.Join(", ", Cases.Select(c => c.DocSee))}}.
                    /// </summary>
                    [{{_compilerGenerated}}]
                    [{{_debuggerBrowsable}}({{_debuggerBrowsableState}}.Never)]
                    [{{_editorBrowsable}}({{_editorBrowsableState}}.Never)]
                    private readonly {{_object}}? {{Value}};

                    /// <summary>
                    /// Returns the string representation of the current value.
                    /// </summary>
                    /// <returns>the string representation of the current value.</returns>
                    [{{_compilerGenerated}}]
                    public override {{_string}} ToString()
                    {
                        return this.{{Value}}?.ToString() ?? "";
                    }

                    /// <inheritdoc />
                    [{{_compilerGenerated}}]
                    public override {{_int}} GetHashCode()
                    {
                        return new {{_valueTuple}}<{{TDiscriminator}}, {{_object}}?>(this.{{Discriminator}}, this.{{Value}}).GetHashCode();
                    }

                    /// <summary>
                    /// Determines whether the two {{DocSee}} instances are equal.
                    /// </summary>
                    /// <param name="left">The {{DocSee}} to compare to <paramref name="right"/>.</param>
                    /// <param name="right">The {{DocSee}} to compare to <paramref name="left"/>.</param>
                    /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise <c>false</c>.</returns>
                    [{{_compilerGenerated}}]
                    public static {{_bool}} Equals({{TUnion}} left, {{TUnion}} right)
                    {
                        {{(IsNonNull ? "" : $$"""
                        if ({{_object}}.ReferenceEquals(left, null))
                        {
                            return {{_object}}.ReferenceEquals(right, null);
                        }
                        """).Indent(8)}}
                        return left.Equals(right);
                    }

                    {{(IsRecord ? "" : $$"""
                    /// <summary>
                    /// Determines whether the two {{DocSee}} instances are equal.
                    /// </summary>
                    /// <param name="left">The {{DocSee}} to compare to <paramref name="right"/>.</param>
                    /// <param name="right">The {{DocSee}} to compare to <paramref name="left"/>.</param>
                    /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are equal; otherwise <c>false</c>.</returns>
                    [{{_compilerGenerated}}]
                    public static {{_bool}} operator ==({{TUnion}} left, {{TUnion}} right)
                    {
                        return Equals(left, right);
                    }

                    /// <summary>
                    /// Determines whether the two {{DocSee}} instances are not equal.
                    /// </summary>
                    /// <param name="left">The {{DocSee}} to compare to <paramref name="right"/>.</param>
                    /// <param name="right">The {{DocSee}} to compare to <paramref name="left"/>.</param>
                    /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are not equal; otherwise <c>false</c>.</returns>
                    [{{_compilerGenerated}}]
                    public static {{_bool}} operator !=({{TUnion}} left, {{TUnion}} right)
                    {
                        return !Equals(left, right);
                    }

                    /// <summary>
                    /// Determines whether the this {{DocSee}} instance is equal to some other <see cref="System.Object" />.
                    /// </summary>
                    /// <param name="other">The value to compare this {{DocSee}} to.</param>
                    /// <returns><c>true</c> if this {{DocSee}} and <paramref name="other"/> are equal; otherwise <c>false</c>.</returns>
                    [{{_compilerGenerated}}]
                    public override {{_bool}} Equals({{_object}}? other)
                    {
                        if (other is {{TUnion}})
                        {
                            return this.Equals({{FromObject("other")}});
                        }

                        switch(this.{{Discriminator}})
                        {
                            {{string.Join("\r\n", Cases.Select(c => $$"""
                            case {{c.Id}}:
                                return other is {{c.TCase}} && {{_object}}.Equals(this.{{Value}}, other);

                            """)).Indent(8)}}
                            default:
                                return false;
                        }
                    }

                    /// <summary>
                    /// Determines whether the this {{DocSee}} instance is equal to another {{DocSee}} instance.
                    /// </summary>
                    /// <param name="other">The {{DocSee}} to compare this {{DocSee}} to.</param>
                    /// <returns><c>true</c> if this {{DocSee}} and <paramref name="other"/> are equal; otherwise <c>false</c>.</returns>
                    [{{_compilerGenerated}}]
                    public {{_bool}} Equals({{TUnion}} other)
                    {
                        {{(IsNonNull ? "" : $$"""
                        if ({{_object}}.ReferenceEquals(other, null))
                        {
                            return false;
                        }
                        """).Indent(4)}}
                        return this.{{Discriminator}} == other.{{Discriminator}}
                            && {{_object}}.Equals(this.{{Value}}, other.{{Value}});
                    }
                    """).Indent(4)}}

                    {{string.Join("\r\n", Cases.Select(c => $$"""

                    /// <summary>
                    /// Creates a new instance of the {{DocSee}} class, using a {{c.DocSee}} as its value.
                    /// </summary>
                    /// <param name="value">The underlying value the {{DocSee}} instance will wrap.</param>
                    [{{_compilerGenerated}}]
                    public {{Name}}({{c.TCase}} value)
                    {
                        this.{{Discriminator}} = {{c.Id}};
                        this.{{Value}} = value;
                    }

                    /// <summary>
                    /// Determines if the current {{DocSee}} instance represents a {{c.DocSee}} or not.
                    /// </summary>
                    /// <param name="value">The {{c.DocSee}} value this wraps if this {{DocSee}} represents a {{c.DocSee}}, otherwise the default value of {{c.DocSee}}.</param>
                    /// <returns><c>true</c> if this {{DocSee}} represents a {{c.DocSee}}; otherwise <c>false</c>.</returns>
                    [{{_compilerGenerated}}]
                    public {{_bool}} {{c.IsCase}}(out {{c.TCase}} value)
                    {
                        if (this.{{Discriminator}} == {{c.Id}})
                        {
                            value = {{c.FromObject($"this.{Value}")}};
                            return true;
                        }
                        else
                        {
                            value = default({{c.TCase}});
                            return false;
                        }
                    }

                    /// <summary>
                    /// Returns the value this {{DocSee}} represents if it is a {{c.DocSee}}; otherwise the default value of {{c.DocSee}}.
                    /// </summary>
                    /// <returns>the value this {{DocSee}} represents if it is a {{c.DocSee}}; otherwise the default value of {{c.DocSee}}.</returns>
                    [{{_compilerGenerated}}]
                    public {{c.NullableTCase}} {{c.CaseOrDefault}}()
                    {
                        if (this.{{Discriminator}} == {{c.Id}})
                        {
                            return {{c.FromObject($"this.{Value}")}};
                        }
                        else
                        {
                            return default({{c.TCase}});
                        }
                    }

                    /// <summary>
                    /// Returns the value this {{DocSee}} represents if it is a {{c.DocSee}}; otherwise <paramref name="default" />.
                    /// </summary>
                    /// <returns>the value this {{DocSee}} represents if it is a {{c.DocSee}}; otherwise <paramref name="default" />.</returns>
                    [{{_compilerGenerated}}]
                    public {{c.TCase}} {{c.CaseOrDefault}}({{c.TCase}} @default)
                    {
                        if (this.{{Discriminator}} == {{c.Id}})
                        {
                            return {{c.FromObject($"this.{Value}")}};
                        }
                        else
                        {
                            return @default;
                        }
                    }

                    /// <summary>
                    /// Returns the value this {{DocSee}} represents if it is a {{c.DocSee}}; otherwise the result of invoking <paramref name="default" />.
                    /// </summary>
                    /// <returns>the value this {{DocSee}} represents if it is a {{c.DocSee}}; otherwise the result of invoking <paramref name="default" />.</returns>
                    [{{_compilerGenerated}}]
                    public {{c.TCase}} {{c.CaseOrDefault}}({{_func}}<{{c.TCase}}> @default)
                    {
                        if (this.{{Discriminator}} == {{c.Id}})
                        {
                            return {{c.FromObject($"this.{Value}")}};
                        }
                        else
                        {
                            return @default();
                        }
                    }

                    {{(c.Kind is CA.TypeKind.Interface ? "" : $$"""
                    /// <summary>
                    /// Creates a new instance of the {{DocSee}} class, using a {{c.DocSee}} as its value.
                    /// </summary>
                    /// <param name="value">The underlying value the {{DocSee}} instance will wrap.</param>
                    [{{_compilerGenerated}}]
                    public static implicit operator {{TUnion}}({{c.TCase}} value)
                    {
                        return new {{TUnion}}(value);
                    }

                    /// <summary>
                    /// Returns the value that <paramref name="value" /> represents if it is a {{c.DocSee}}
                    /// </summary>
                    /// <returns>the value that <paramref name="value" /> represents if it is a {{c.DocSee}}.</returns>
                    /// <exception cref="{{_invalidCastException}}">Thrown when the value represented by <paramref name="value" /> is not a {{c.DocSee}}.</exception>
                    [{{_compilerGenerated}}]
                    public static explicit operator {{c.TCase}}({{TUnion}} value)
                    {
                        if (value.{{Discriminator}} == {{c.Id}})
                        {
                            return {{c.FromObject($"value.{Value}")}};
                        }
                        else
                        {
                            throw new {{_invalidCastException}}();
                        }
                    }
                    """)}}
                    """)).Indent(4)}}

                    /// <summary>
                    /// Invokes one of the delegates based on what type this {{DocSee}} represents.
                    /// <list type="table">
                    ///     <listheader>
                    ///         <term>Delegate.</term>
                    ///         <description>When it will be invoked.</description>
                    ///     </listheader>
                    {{string.Join("\r\n", Cases.Select(c => $$"""
                    ///     <item>
                    ///         <term><paramref name="case{{c.Name}}" /></term>
                    ///         <description>Invoked when this {{DocSee}} represents a {{c.DocSee}}.</description>
                    ///     </item>
                    """)).Indent(4)}}
                    ///     <item>
                    ///         <term><paramref name="default" /></term>
                    ///         <description>Invoked when the delegate that would have otherwise been invoked was null.</description>
                    ///     </item>
                    /// </list>
                    /// </summary>
                    /// <param name="default"></param>
                    {{string.Join("\r\n", Cases.Select(c => $$"""
                    /// <param name="case{{c.Name}}">The delegate to invoke when the {{DocSee}} represents a {{c.DocSee}}.</param>
                    """)).Indent(4)}}
                    /// <exception cref="System.InvalidOperationException">Thrown when this {{DocSee}} is not a valid instance. This means that the <see cref="{{Discriminator}}" /> has been tampered with via reflection, or {{DocSee}} is a struct and this is the default value of {{DocSee}}.</exception>
                    /// <exception cref="System.ArgumentNullException">Thrown when both the delegate that should have been invoked and <paramref name="default" /> are null.</exception>
                    [{{_compilerGenerated}}]
                    public void {{Switch}}
                    ({{string.Join(",", Cases
                            .Select(c => $"\r\n{_action}<{c.TCase}>? case{c.Name} = null")
                            .Prepend($"\r\n{_action} @default")
                        ).Indent(8)}}
                    )
                    {
                        switch(this.{{Discriminator}})
                        {
                            case 0:
                                throw new {{_invalidOperationException}}("Union is not initialized");

                            {{string.Join("\r\n", Cases.Select(c => $$"""
                            case {{c.Id}}:
                                if (!{{_object}}.ReferenceEquals(case{{c.Name}}, null))
                                {
                                    case{{c.Name}}.Invoke({{c.FromObject($"this.{Value}")}});
                                }
                                else if (!{{_object}}.ReferenceEquals(@default, null))
                                {
                                    @default.Invoke();
                                }
                                else
                                {
                                    throw new {{_argumentNullException}}(nameof(@default));
                                }
                                break;

                            """)).Indent(12)}}
                            default:
                                throw new {{_invalidOperationException}}("Union is not valid");
                        }
                    }

                    /// <summary>
                    /// Invokes one of the delegates based on what type this {{DocSee}} represents.
                    /// <list type="table">
                    ///     <listheader>
                    ///         <term>Delegate.</term>
                    ///         <description>When it will be invoked.</description>
                    ///     </listheader>
                    {{string.Join("\r\n", Cases.Select(c => $$"""
                    ///     <item>
                    ///         <term><paramref name="case{{c.Name}}" /></term>
                    ///         <description>Invoked when this {{DocSee}} represents a {{c.DocSee}}.</description>
                    ///     </item>
                    """)).Indent(4)}}
                    /// </list>
                    /// </summary>
                    {{string.Join("\r\n", Cases.Select(c => $$"""
                    /// <param name="case{{c.Name}}">The delegate to invoke when the {{DocSee}} represents a {{c.DocSee}}.</param>
                    """)).Indent(4)}}
                    /// <exception cref="System.InvalidOperationException">Thrown when this {{DocSee}} is not a valid instance. This means that the <see cref="{{Discriminator}}" /> has been tampered with via reflection, or {{DocSee}} is a struct and this is the default value of {{DocSee}}.</exception>
                    /// <exception cref="System.ArgumentNullException">Thrown when the delegate that should have been invoked is null.</exception>
                    [{{_compilerGenerated}}]
                    public void {{Switch}}
                    ({{string.Join(",", Cases
                            .Select(c => $"\r\n{_action}<{c.TCase}> case{c.Name}")
                        ).Indent(8)}}
                    )
                    {
                        switch(this.{{Discriminator}})
                        {
                            case 0:
                                throw new {{_invalidOperationException}}("Union is not initialized");

                            {{string.Join("\r\n", Cases.Select(c => $$"""
                            case {{c.Id}}:
                                if (!{{_object}}.ReferenceEquals(case{{c.Name}}, null))
                                {
                                    case{{c.Name}}.Invoke({{c.FromObject($"this.{Value}")}});
                                }
                                else
                                {
                                    throw new {{_argumentNullException}}(nameof(case{{c.Name}}));
                                }
                                break;

                            """)).Indent(12)}}
                            default:
                                throw new {{_invalidOperationException}}("Union is not valid");
                        }
                    }
                    /// <summary>
                    /// Invokes one of the delegates based on what type this {{DocSee}} represents and returns its result.
                    /// <list type="table">
                    ///     <listheader>
                    ///         <term>Delegate.</term>
                    ///         <description>When it will be invoked.</description>
                    ///     </listheader>
                    {{string.Join("\r\n", Cases.Select(c => $$"""
                    ///     <item>
                    ///         <term><paramref name="case{{c.Name}}" /></term>
                    ///         <description>Invoked when this {{DocSee}} represents a {{c.DocSee}}.</description>
                    ///     </item>
                    """)).Indent(4)}}
                    ///     <item>
                    ///         <term><paramref name="default" /></term>
                    ///         <description>Invoked when the delegate that would have otherwise been invoked was null.</description>
                    ///     </item>
                    /// </list>
                    /// </summary>
                    /// <param name="default"></param>
                    {{string.Join("\r\n", Cases.Select(c => $$"""
                    /// <param name="case{{c.Name}}">The delegate to invoke when the {{DocSee}} represents a {{c.DocSee}}.</param>
                    """)).Indent(4)}}
                    /// <returns>the result of invoking the relevant delegate.</returns>
                    /// <exception cref="System.InvalidOperationException">Thrown when this {{DocSee}} is not a valid instance. This means that the <see cref="{{Discriminator}}" /> has been tampered with via reflection, or {{DocSee}} is a struct and this is the default value of {{DocSee}}.</exception>
                    /// <exception cref="System.ArgumentNullException">Thrown when both the delegate that should have been invoked and <paramref name="default" /> are null.</exception>
                    [{{_compilerGenerated}}]
                    public {{TMatchResult}} {{Match}}<{{TMatchResult}}>
                    ({{string.Join(",", Cases
                            .Select(c => $"\r\n{_func}<{c.TCase}, {TMatchResult}>? case{c.Name} = null")
                            .Prepend($"\r\n{_func}<{TMatchResult}> @default")
                        ).Indent(8)}}
                    )
                    {
                        switch(this.{{Discriminator}})
                        {
                            case 0:
                                throw new {{_invalidOperationException}}("Union is not initialized");

                            {{string.Join("\r\n", Cases.Select(c => $$"""
                            case {{c.Id}}:
                                if (!{{_object}}.ReferenceEquals(case{{c.Name}}, null))
                                {
                                    return case{{c.Name}}.Invoke({{c.FromObject($"this.{Value}")}});
                                }
                                else if (!{{_object}}.ReferenceEquals(@default, null))
                                {
                                    return @default.Invoke();
                                }
                                else
                                {
                                    throw new {{_argumentNullException}}(nameof(@default));
                                }

                            """)).Indent(12)}}
                            default:
                                throw new {{_invalidOperationException}}("Union is not valid");
                        }
                    }
                    /// <summary>
                    /// Invokes one of the delegates based on what type this {{DocSee}} represents and returns its result.
                    /// <list type="table">
                    ///     <listheader>
                    ///         <term>Delegate.</term>
                    ///         <description>When it will be invoked.</description>
                    ///     </listheader>
                    {{string.Join("\r\n", Cases.Select(c => $$"""
                    ///     <item>
                    ///         <term><paramref name="case{{c.Name}}" /></term>
                    ///         <description>Invoked when this {{DocSee}} represents a {{c.DocSee}}.</description>
                    ///     </item>
                    """)).Indent(4)}}
                    /// </list>
                    /// </summary>
                    {{string.Join("\r\n", Cases.Select(c => $$"""
                    /// <param name="case{{c.Name}}">The delegate to invoke when the {{DocSee}} represents a {{c.DocSee}}.</param>
                    """)).Indent(4)}}
                    /// <returns>the result of invoking the relevant delegate.</returns>
                    /// <exception cref="System.InvalidOperationException">Thrown when this {{DocSee}} is not a valid instance. This means that the <see cref="{{Discriminator}}" /> has been tampered with via reflection, or {{DocSee}} is a struct and this is the default value of {{DocSee}}.</exception>
                    /// <exception cref="System.ArgumentNullException">Thrown when the delegate that should have been invoked is null.</exception>
                    [{{_compilerGenerated}}]
                    public {{TMatchResult}} {{Match}}<{{TMatchResult}}>
                    ({{string.Join(",", Cases
                            .Select(c => $"\r\n{_func}<{c.TCase}, {TMatchResult}> case{c.Name}")
                        ).Indent(8)}}
                    )
                    {
                        switch(this.{{Discriminator}})
                        {
                            case 0:
                                throw new {{_invalidOperationException}}("Union is not initialized");

                            {{string.Join("\r\n", Cases.Select(c => $$"""
                            case {{c.Id}}:
                                if (!{{_object}}.ReferenceEquals(case{{c.Name}}, null))
                                {
                                    return case{{c.Name}}.Invoke({{c.FromObject($"this.{Value}")}});
                                }
                                else
                                {
                                    throw new {{_argumentNullException}}(nameof(case{{c.Name}}));
                                }

                            """)).Indent(12)}}
                            default:
                                throw new {{_invalidOperationException}}("Union is not valid");
                        }
                    }
                }
                """;
        }

        private static string ComputeDiscriminatorType(Sequence<UnionCase> cases)
        {
            return cases.Length switch
            {
                < byte.MaxValue => _byte,
                < ushort.MaxValue => _ushort,
                _ => _uint
            };
        }

        private static string ComputeMatchResultType(Union model)
        {
            const string name = "TMatchResult";
            var known = model.Id.TypeParameters.Select(p => p.Name).ToImmutableHashSet();
            if (!known.Contains(name))
                return name;

            for (var i = 0; true; i++)
            {
                if (!known.Contains(name + i))
                    return name + i;
            }
        }

        private static string GetKindSource(TypeDefinition kind)
        {
            var segments = new List<string>();
            if (kind.Accessibility is not CA.Accessibility.NotApplicable)
                segments.Add(SyntaxFacts.GetText(kind.Accessibility));

            if (kind.Kind == CA.TypeKind.Struct)
                segments.Add(SyntaxFacts.GetText(SyntaxKind.ReadOnlyKeyword));

            segments.Add(SyntaxFacts.GetText(SyntaxKind.PartialKeyword));

            if (kind.IsRecord)
                segments.Add(SyntaxFacts.GetText(SyntaxKind.RecordKeyword));

            segments.Add(SyntaxFacts.GetText(kind.Kind switch
            {
                CA.TypeKind.Struct => SyntaxKind.StructKeyword,
                CA.TypeKind.Interface => SyntaxKind.InterfaceKeyword,
                CA.TypeKind.Class => SyntaxKind.ClassKeyword,
                _ => throw new NotSupportedException($"Unsupported union type kind {kind.Kind}")
            }));

            return string.Join(" ", segments);
        }

        private static string RenderTypeConstraints(Sequence<TypeParameter> typeParameters)
        {
            return string.Join("", typeParameters
                .Where(p => p.Constraints.Length > 0)
                .Select(p => $"\r\n    where {p.Name} : {string.Join(", ", p.Constraints)}"));
        }

        private string Cast(string value, string type, CA.TypeKind kind)
        {
            if (!_useUnsafe)
                return $"(({type}){value})";

            return kind switch
            {
                CA.TypeKind.Enum or CA.TypeKind.Struct => $"{_unsafe}.Unbox<{type}>({value}!)",
                CA.TypeKind.Class or CA.TypeKind.Delegate => $"{_unsafe}.As<{type}>({value})",
                _ => $"(({type}){value})",
            };
        }

        private string FromObject(string value)
        {
            return Cast(value, TUnion, Kind);
        }

        private sealed class UnionCaseRenderContext
        {
            public string CaseOrDefault { get; }

            public string DocSee { get; }

            public uint Id { get; }

            public string IsCase { get; }

            public CA.TypeKind Kind { get; }

            public string Name { get; }

            public string NullableTCase { get; }

            public string TCase { get; }

            public UnionRenderContext Union { get; }

            public UnionCaseRenderContext(UnionRenderContext union, UnionCase @case, uint id)
            {
                Union = union;
                Id = id;
                TCase = Helpers.Render(@case.Id);
                NullableTCase = TCase + (IsValueType(@case.Definition.Kind) ? "" : "?");
                IsCase = @case.Config.IsCaseName;
                CaseOrDefault = @case.Config.CaseOrDefaultName;
                Kind = @case.Definition.Kind;
                Name = @case.Id.Name;
                DocSee = Helpers.RenderDocSee(@case.Id);
            }

            public string FromObject(string value)
            {
                return Union.Cast(value, TCase, Kind);
            }

            private static bool IsValueType(CA.TypeKind kind)
            {
                return kind is CA.TypeKind.Struct or CA.TypeKind.Enum;
            }
        }
    }
}