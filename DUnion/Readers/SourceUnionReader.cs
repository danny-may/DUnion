using DUnion.Generators;
using DUnion.Models;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CA = Microsoft.CodeAnalysis;

namespace DUnion.Readers;

internal abstract class SourceUnionReader : INamedSymbolUnionReader
{
    public IUnionGenerator Read(UnionConfig config, CA.INamedTypeSymbol symbol, CA.GeneratorAttributeSyntaxContext context)
    {
        var location = context.TargetNode.GetLocation();

        var diagnostics = new List<Diagnostic>();
        if (!TryReadUnionSymbol(symbol, diagnostics.Add, out var type))
        {
            return new EmptyUnionGenerator(location)
                .WithDiagnostics(diagnostics);
        }

        var containers = new List<TypeRef>();
        if (!symbol.ContainingTypes()
            .Select(s => TryReadContainerSymbol(s, diagnostics.Add, containers.Add))
            .Aggregate(true, (a, b) => a && b))
        {
            return new EmptyUnionGenerator(location)
                .WithDiagnostics(diagnostics);
        }

        var cases = new List<TypeRef>();
        if (!symbol.GetTypeMembers()
            .Select(s => TryReadCaseSymbol(s, symbol, diagnostics.Add, cases.Add))
            .Aggregate(true, (a, b) => a && b))
        {
            return new EmptyUnionGenerator(location)
                .WithDiagnostics(diagnostics);
        }

        if (cases.Count == 0)
        {
            diagnostics.Add(Diagnostics.EmptyUnion(symbol));
            return new EmptyUnionGenerator(location)
                .WithDiagnostics(diagnostics);
        }

        return Create(new(symbol.GetNamespace(), new(containers), type, config, new(cases), location))
            .WithDiagnostics(diagnostics);
    }

    protected abstract bool CheckCaseType(CA.INamedTypeSymbol caseSymbol, CA.INamedTypeSymbol unionSymbol, Action<Diagnostic> report);

    protected abstract bool CheckContainerType(CA.INamedTypeSymbol containerSymbol, Action<Diagnostic> report);

    protected abstract bool CheckUnionType(CA.INamedTypeSymbol unionSymbol, Action<Diagnostic> report);

    protected abstract IUnionGenerator Create(Union union);

    protected virtual bool IgnoreUnionCase(CA.INamedTypeSymbol caseSymbol, CA.INamedTypeSymbol unionSymbol, Action<Diagnostic> report)
    {
        if (caseSymbol.GetAttributes().Any(a => a.AttributeClass?.ToDisplayString() == Constants.DUnionExcludeFullName))
            return true;

        var result = false;

        if (caseSymbol.DeclaredAccessibility < unionSymbol.DeclaredAccessibility && (result = true))
            report(Diagnostics.IgnoringLessAccessibleType(caseSymbol, unionSymbol));

        return result;
    }

    private static PTypeKind? GetTypeKind(CA.INamedTypeSymbol type)
    {
        return type switch
        {
            { TypeKind: CA.TypeKind.Interface } => PTypeKind.Interface,
            { TypeKind: CA.TypeKind.Struct, IsRecord: false } => PTypeKind.Struct,
            { TypeKind: CA.TypeKind.Struct, IsRecord: true } => PTypeKind.RecordStruct,
            { TypeKind: CA.TypeKind.Class, IsRecord: false, IsStatic: false } => PTypeKind.Class,
            { TypeKind: CA.TypeKind.Class, IsRecord: true, IsStatic: false } => PTypeKind.RecordClass,
            { TypeKind: CA.TypeKind.Enum } => PTypeKind.Enum,
            { TypeKind: CA.TypeKind.Delegate } => PTypeKind.Delegate,
            _ => null
        };
    }

    private bool TryReadCaseSymbol(CA.INamedTypeSymbol caseSymbol, CA.INamedTypeSymbol unionSymbol, Action<Diagnostic> report, Action<TypeRef> add)
    {
        if (IgnoreUnionCase(caseSymbol, unionSymbol, report))
            return true;

        if (caseSymbol.TypeParameters.Length > 0)
        {
            report(Diagnostics.CannotUseGenericCase(caseSymbol, unionSymbol));
            return false;
        }

        if (!CheckCaseType(caseSymbol, unionSymbol, report) || GetTypeKind(caseSymbol) is not { } kind)
            return false;

        add(new(kind, caseSymbol.ToSignature(), caseSymbol.Name));
        return true;
    }

    private bool TryReadContainerSymbol(CA.INamedTypeSymbol containerSymbol, Action<Diagnostic> report, Action<TypeRef> add)
    {
        if (!CheckContainerType(containerSymbol, report) || GetTypeKind(containerSymbol) is not { } kind)
            return false;

        add(new(kind, containerSymbol.ToSignature(), containerSymbol.Name));
        return true;
    }

    private bool TryReadUnionSymbol(CA.INamedTypeSymbol unionSymbol, Action<Diagnostic> report, out TypeRef type)
    {
        if (!CheckUnionType(unionSymbol, report) || GetTypeKind(unionSymbol) is not { } kind)
        {
            type = default;
            return false;
        }

        type = new(kind, unionSymbol.ToSignature(), unionSymbol.Name);
        return true;
    }

    protected abstract record SourceUnionGenerator(Union Union) : IUnionGenerator
    {
        private const string _action = "global::System.Action";
        private const string _argumentNullException = "global::System.ArgumentNullException";
        protected const string CompilerGenerated = "global::System.Runtime.CompilerServices.CompilerGeneratedAttribute";
        protected const string EditorBrowsable = "global::System.ComponentModel.EditorBrowsableAttribute";
        protected const string EditorBrowsableStateNever = "global::System.ComponentModel.EditorBrowsableState.Never";
        protected const string DebuggerBrowsable = "global::System.Diagnostics.DebuggerBrowsableAttribute";
        protected const string DebuggerBrowsableStateNever = "global::System.Diagnostics.DebuggerBrowsableState.Never";
        private const string _func = "global::System.Func";
        private const string _invalidOperationException = "global::System.InvalidOperationException";
        private const string _referenceEquals = "global::System.Object.ReferenceEquals";
        private const string _maybeNullWhen = "global::System.Diagnostics.CodeAnalysis.MaybeNullWhen";
        private const string _t = "TMatchResult";
        private static readonly Regex _newlines = new(@"\r?\n", RegexOptions.Compiled);

        public Location Location => Union.Location;

        protected virtual string DiscriminatorValue => Union.Config.Discriminator ?? "Discriminator";
        protected abstract string UnderlyingType { get; }
        protected virtual string UnderlyingValue => Union.Config.UnderlyingValue ?? "UnderlyingValue";
        protected virtual string SwitchName => Union.Config.Switch ?? "Switch";
        protected virtual string MatchName => Union.Config.Match ?? "Match";

        public void Execute(CA.SourceProductionContext context)
        {
            context.AddSource(FileName, SourceText.From(FileContent, Encoding.UTF8));
        }

        protected virtual string DiscriminatorType { get; } = Union.Cases.Length switch
        {
            <= byte.MaxValue => "global::System.Byte",
            <= ushort.MaxValue => "global::System.UInt16",
            _ => "global::System.UInt32"
        };

        protected virtual string FileContent => _newlines.Replace($$"""
            {{Header}}
            {{Usings}}

            {{NamespaceBlock(Union.ContainingTypes.Aggregate(Body, (body, container) => $$"""
            {{Partial(container.Kind)}} {{container.Symbol}}
            {
                {{Indent(1, body)}}
            }
            """))}}
            """, Environment.NewLine);

        protected virtual string Header => $$"""
            {{Constants.AutoGeneratedHeader}}
            #pragma warning disable
            #nullable enable
            """;

        protected virtual string Usings => $$"""

            """;

        protected static string DocSee(TypeRef type)
        {
            return $"<see cref=\"{type.Symbol.Replace('<', '{').Replace('>', '}')}\"/>";
        }

        protected static string Indent(int tabCount, string lines)
        {
            return string.Join("\n" + new string(' ', tabCount * 4), lines.Split('\n'));
        }

        protected static string Indent(int tabCount, IEnumerable<string> lines)
        {
            return string.Join("\n" + new string(' ', tabCount * 4), lines.SelectMany(l => l.Split('\n')));
        }

        protected static string Partial(PTypeKind kind)
        {
            return kind switch
            {
                PTypeKind.Struct => "partial struct",
                PTypeKind.RecordStruct => "partial record struct",
                PTypeKind.Class => "partial class",
                PTypeKind.RecordClass => "partial record",
                PTypeKind.Interface => "partial interface",
                _ => throw new ArgumentException($"{kind} cannot be marked as partial", nameof(kind))
            };
        }

        protected virtual string ThrowUnsupportedDiscriminator => $$"""
            throw new {{_invalidOperationException}}($"Unsupported discriminator value {{{DiscriminatorValue}}}");
            """;
        protected virtual string ThrowNotInitialized => $$"""
            throw new {{_invalidOperationException}}("Union has not been initialized");
            """;

        /// <summary>
        ///
        /// </summary>
        /// <param name="case"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        protected virtual string CaseId(TypeRef @case)
        {
            return Union.Cases.IndexOf(@case) switch
            {
                -1 => throw new ArgumentException("Unknown case", nameof(@case)),
                var index => (index + 1).ToString()
            };
        }

        protected virtual string IsCaseName(TypeRef @case)
        {
            var template = Union.Config.IsCase ?? "Is{0}";
            return string.Format(template, @case.Name);
        }

        protected string IsCase => $$"""

            {{Indent(0, Union.Cases.Select(c => $$"""
            /// <summary>
            /// If the current value is a {{DocSee(c)}} then <paramref name="as{{c.Name}}"/> will be set to it and <c>true</c> will be returned,
            /// otherwise <paramref name="{{c.Name}}"/> will be set to <c>default({{DocSee(c)}})</c> and <c>false</c> will be returned
            /// </summary>
            /// <param name="as{{c.Name}}">The current value if this is a {{DocSee(c)}}, otherwise <c>default({{DocSee(c)}})</c></param>
            /// <returns><c>true</c> if the current value is a {{DocSee(c)}}, otherwise <c>false</c></returns>
            public bool {{IsCaseName(c)}}([{{_maybeNullWhen}}(true)]out {{c.Symbol}} as{{c.Name}})
            {
                if (this.@{{DiscriminatorValue}} != {{CaseId(c)}})
                {
                    as{{c.Name}} = default({{c.Symbol}});
                    return false;
                }
                as{{c.Name}} = ({{c.Symbol}})this.@{{UnderlyingValue}};
                return true;
            }

            """))}}
            """;

        protected string Switch => $$"""
            /// <summary>
            /// Calls the appropriate delegate for the current type.
            /// <list type="bullet">
            {{Indent(0, Union.Cases.Select(c => $$"""
            /// <item>Calls <paramref name="case{{c.Name}}"/> if this is a {{DocSee(c)}}</item>
            """))}}
            /// <item>Calls <paramref name="default"/> if the appropriate case* delegate wasnt provided.</item>
            /// </list>
            /// </summary>
            {{Indent(0, Union.Cases.Select(c => $$"""
            /// <param name="case{{c.Name}}">Called if this is a {{DocSee(c)}}</param>
            """))}}
            /// <param name="default">Called if a delegate was not provided for this type</param>
            [{{CompilerGenerated}}]
            public void {{SwitchName}}({{_action}} @default, {{string.Join(", ", Union.Cases.Select(c => $"{_action}<{c.Symbol}>? case{c.Name} = null"))}})
            {
                switch (this.@{{DiscriminatorValue}})
                {
                    case 0:
                        {{ThrowNotInitialized}}
                    {{Indent(2, Union.Cases.Select(c => $$"""
                    case {{CaseId(c)}}:
                        if (!{{_referenceEquals}}(case{{c.Name}}, null))
                            case{{c.Name}}(({{c.Symbol}})this.@{{UnderlyingValue}}!);
                        else if (!{{_referenceEquals}}(@default, null))
                            @default();
                        else
                            throw new {{_argumentNullException}}(nameof(@default));
                        break;
                    """))}}
                    default:
                        {{ThrowUnsupportedDiscriminator}}
                }
            }
            """;

        protected string SwitchAll => $$"""
            /// <summary>
            /// Calls the appropriate delegate for the current type.
            /// <list type="bullet">
            {{Indent(0, Union.Cases.Select(c => $$"""
            /// <item>Calls <paramref name="case{{c.Name}}"/> if this is a {{DocSee(c)}}</item>
            """))}}
            /// </list>
            /// </summary>
            {{Indent(0, Union.Cases.Select(c => $$"""
            /// <param name="case{{c.Name}}">Called if this is a {{DocSee(c)}}</param>
            """))}}
            [{{CompilerGenerated}}]
            public void {{SwitchName}}({{string.Join(", ", Union.Cases.Select(c => $"{_action}<{c.Symbol}> case{c.Symbol}"))}})
            {
                switch (this.@{{DiscriminatorValue}})
                {
                    case 0:
                        {{ThrowNotInitialized}}
                    {{Indent(2, Union.Cases.Select(c => $$"""
                    case {{CaseId(c)}}:
                        if ({{_referenceEquals}}(case{{c.Name}}, null))
                            throw new {{_argumentNullException}}(nameof(case{{c.Name}}));
                        case{{c.Name}}(({{c.Symbol}})this.@{{UnderlyingValue}}!);
                        break;
                    """))}}
                    default:
                        {{ThrowUnsupportedDiscriminator}}
                }
            }
            """;

        protected string MatchAll => $$"""
            /// <summary>
            /// Calls the appropriate delegate for the current type.
            /// <list type="bullet">
            {{Indent(0, Union.Cases.Select(c => $$"""
            /// <item>Calls <paramref name="case{{c.Name}}"/> if this is a {{DocSee(c)}}</item>
            """))}}
            /// </list>
            /// </summary>
            {{Indent(0, Union.Cases.Select(c => $$"""
            /// <param name="case{{c.Name}}">Called if this is a {{DocSee(c)}}</param>
            """))}}
            /// <returns>The value returned from the matched delegate</returns>
            [{{CompilerGenerated}}]
            public {{_t}} {{MatchName}}<{{_t}}>({{string.Join(", ", Union.Cases.Select(c => $"{_func}<{c.Symbol}, {_t}> case{c.Name}"))}})
            {
                switch (this.@{{DiscriminatorValue}})
                {
                    case 0:
                        {{ThrowNotInitialized}}
                    {{Indent(2, Union.Cases.Select(c => $$"""
                    case {{CaseId(c)}}:
                        if ({{_referenceEquals}}(case{{c.Name}}, null))
                            throw new {{_argumentNullException}}(nameof(case{{c.Name}}));
                        return case{{c.Name}}(({{c.Symbol}})this.@{{UnderlyingValue}}!);
                    """))}}
                    default:
                        {{ThrowUnsupportedDiscriminator}}
                }
            }
            """;

        protected string Match => $$"""
            /// <summary>
            /// Calls the appropriate delegate for the current type.
            /// <list type="bullet">
            {{Indent(0, Union.Cases.Select(c => $$"""
            /// <item>Calls <paramref name="case{{c.Name}}"/> if this is a {{DocSee(c)}}</item>
            """))}}
            /// <item>Calls <paramref name="default"/> if the appropriate case* delegate wasnt provided.</item>
            /// </list>
            /// </summary>
            {{Indent(0, Union.Cases.Select(c => $$"""
            /// <param name="case{{c.Name}}">Called if this is a {{DocSee(c)}}</param>
            """))}}
            /// <param name="default">Called if a delegate was not provided for this type</param>
            /// <returns>The value returned from the matched delegate</returns>
            [{{CompilerGenerated}}]
            public {{_t}} {{MatchName}}<{{_t}}>({{_func}}<{{_t}}> @default, {{string.Join(", ", Union.Cases.Select(c => $"{_func}<{c.Name}, {_t}>? case{c.Symbol} = null"))}})
            {
                switch (this.@{{DiscriminatorValue}})
                {
                    case 0:
                        {{ThrowNotInitialized}}
                    {{Indent(2, Union.Cases.Select(c => $$"""
                    case {{CaseId(c)}}:
                        if (!{{_referenceEquals}}(case{{c.Name}}, null))
                            return case{{c.Name}}(({{c.Symbol}})this.@{{UnderlyingValue}}!);
                        if (!{{_referenceEquals}}(@default, null))
                            return @default();
                        throw new {{_argumentNullException}}(nameof(@default));
                    """))}}
                    default:
                        {{ThrowUnsupportedDiscriminator}}
                }
            }
            """;

        private string NamespaceBlock(string content)
        {
            return Union.Namespace is { Length: > 0 }
                ? $$"""
                    namespace {{Union.Namespace}}
                    {
                        {{Indent(1, content)}}
                    }
                    """
                : content;
        }

        protected abstract string Body { get; }

        private static readonly Regex _genericGroup = new(@"(<.*?>)", RegexOptions.Compiled);
        protected virtual string FileName
        {
            get
            {
                var name = new StringBuilder();
                if (Union.Namespace is { Length: > 0 })
                {
                    name.Append(Union.Namespace);
                    name.Append(".");
                }

                for (var i = Union.ContainingTypes.Length - 1; i >= 0; i--)
                {
                    name.Append(Union.ContainingTypes[i].Symbol);
                    name.Append(".");
                }
                name.Append(Union.Type.Symbol);
                name.Append(".");
                name.Append("DUnion.g.cs");
                return _genericGroup.Replace(name.ToString(), m => $"`{m.Value.Where(c => c == ',').Count() + 1}");
            }
        }

        public abstract bool Equals(IUnionGenerator other);
    }
}