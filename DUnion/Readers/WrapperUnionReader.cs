using DUnion.Generators;
using DUnion.Models;
using System;
using System.Linq;
using CA = Microsoft.CodeAnalysis;

namespace DUnion.Readers;

internal sealed class WrapperUnionReader : SourceUnionReader
{
    protected override bool CheckCaseType(CA.INamedTypeSymbol caseSymbol, CA.INamedTypeSymbol unionSymbol, Action<Diagnostic> report)
    {
        if (caseSymbol.IsRefLikeType && caseSymbol.TypeKind == CA.TypeKind.Struct)
            report(Diagnostics.CaseCannotBeARefStruct(caseSymbol, UnionKind.Wrapper));
        else
            return true;
        return false;
    }

    protected override bool CheckContainerType(CA.INamedTypeSymbol containerSymbol, Action<Diagnostic> report)
    {
        return true;
    }

    protected override bool CheckUnionType(CA.INamedTypeSymbol unionSymbol, Action<Diagnostic> report)
    {
        return true;
    }

    protected override IUnionGenerator Create(Union union)
    {
        return new WrapperUnionGenerator(union);
    }

    private sealed record WrapperUnionGenerator(Union Union) : SourceUnionGenerator(Union)
    {
        private const string _invalidCastException = "global::System.InvalidCastException";

        public override bool Equals(IUnionGenerator other)
        {
            return Equals(other as WrapperUnionGenerator);
        }

        protected override string Body => $$"""
            {{Partial(Union.Type.Kind)}} {{Union.Type.Symbol}}
            {
                /// <summary>A value used to discriminate what this instance represents.</summary>
                [{{CompilerGenerated}}]
                [{{DebuggerBrowsable}}({{DebuggerBrowsableStateNever}})]
                [{{EditorBrowsable}}({{EditorBrowsableStateNever}})]
                public {{DiscriminatorType}} @{{DiscriminatorValue}} { get; }

                /// <summary>The underlying value this represents.</summary>
                [{{CompilerGenerated}}]
                [{{EditorBrowsable}}({{EditorBrowsableStateNever}})]
                public {{UnderlyingType}} @{{UnderlyingValue}} { get; }

                [{{CompilerGenerated}}]
                private {{Union.Type.Name}}({{DiscriminatorType}} discriminator, {{UnderlyingType}} value)
                {
                    this.@{{DiscriminatorValue}} = discriminator;
                    this.@{{UnderlyingValue}} = value;
                }
                {{Indent(1, Union.Cases.Select(c => $$"""

                /// <summary>
                /// Creates a {{DocSee(Union.Type)}} which wraps a {{DocSee(c)}}.
                /// </summary>
                /// <param name="value">The {{DocSee(c)}} to wrap</param>
                [{{CompilerGenerated}}]
                public {{Union.Type.Name}}({{c.Symbol}} value) : this({{CaseId(c)}}, value)
                {
                }
                """))}}

                {{Indent(1, Switch)}}

                {{Indent(1, Match)}}

                {{Indent(1, SwitchAll)}}

                {{Indent(1, MatchAll)}}

                {{Indent(1, IsCase)}}
                {{Indent(1, Union.Cases.Where(c => c.Kind is not PTypeKind.Interface).Select(c => $$"""

                [{{CompilerGenerated}}]
                public static implicit operator {{Union.Type.Symbol}}({{c.Symbol}} value)
                {
                    return new {{Union.Type.Symbol}}(value);
                }

                [{{CompilerGenerated}}]
                public static explicit operator {{c.Symbol}}({{Union.Type.Symbol}} value)
                {
                    if (value.@{{DiscriminatorValue}} != {{CaseId(c)}})
                        throw new {{_invalidCastException}}();
                    return ({{c.Symbol}})value.@{{UnderlyingValue}}!;
                }
                """))}}
            }
            """;

        protected override string UnderlyingType => "global::System.Object";
    }
}