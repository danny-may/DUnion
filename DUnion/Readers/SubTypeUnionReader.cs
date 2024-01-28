using DUnion.Generators;
using DUnion.Models;
using System;
using System.Linq;
using CA = Microsoft.CodeAnalysis;

namespace DUnion.Readers;

internal sealed class SubTypeUnionReader : SourceUnionReader
{
    protected override bool CheckCaseType(CA.INamedTypeSymbol caseSymbol, CA.INamedTypeSymbol unionSymbol, Action<Diagnostic> report)
    {
        if (caseSymbol.TypeKind is not CA.TypeKind.Class)
            report(Diagnostics.CannotSetBaseTypeOfNonClass(caseSymbol, unionSymbol));
        else if (caseSymbol.IsRecord && !unionSymbol.IsRecord)
            report(Diagnostics.CannotSetRecordBaseTypeNonRecord(caseSymbol, unionSymbol));
        else if (!caseSymbol.IsRecord && unionSymbol.IsRecord)
            report(Diagnostics.CannotSetNonRecordBaseTypeRecord(caseSymbol, unionSymbol));
        else if (!CanBeMadeToInheritFromUnionType(caseSymbol, unionSymbol))
            report(Diagnostics.CannotSubTypeWhenBaseTypeAlreadySet(caseSymbol, unionSymbol));
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
        var hasErrors = false;
        if (unionSymbol.TypeKind is not CA.TypeKind.Class && (hasErrors = true))
            report(Diagnostics.CannotUseNonClassAsBaseType(unionSymbol));
        else if (unionSymbol.IsSealed && (hasErrors = true))
            report(Diagnostics.CannotSubTypeSealed(unionSymbol));
        return !hasErrors;
    }

    protected override IUnionGenerator Create(Union union)
    {
        return new SubTypeUnionGenerator(union);
    }

    private static bool CanBeMadeToInheritFromUnionType(CA.INamedTypeSymbol caseSymbol, CA.INamedTypeSymbol unionSymbol)
    {
        return caseSymbol.BaseType is null or { SpecialType: CA.SpecialType.System_Object }
            || CA.SymbolEqualityComparer.Default.Equals(caseSymbol.BaseType, unionSymbol);
    }

    private sealed record SubTypeUnionGenerator(Union Union) : SourceUnionGenerator(Union)
    {
        public override bool Equals(IUnionGenerator other)
        {
            return Equals(other as SubTypeUnionGenerator);
        }
        protected override string UnderlyingType => Union.Type.Symbol;

        protected override string Body => $$"""
            abstract {{Partial(Union.Type.Kind)}} {{Union.Type.Symbol}}
            {
                /// <summary>A value used to discriminate what this instance represents.</summary>
                [{{CompilerGenerated}}]
                [{{DebuggerBrowsable}}({{DebuggerBrowsableStateNever}})]
                [{{EditorBrowsable}}({{EditorBrowsableStateNever}})]
                public abstract {{DiscriminatorType}} @{{DiscriminatorValue}} { get; }

                public {{UnderlyingType}} @{{UnderlyingValue}} => this;

                [{{CompilerGenerated}}]
                private {{Union.Type.Name}}()
                {
                }

                {{Indent(1, Switch)}}

                {{Indent(1, Match)}}

                {{Indent(1, SwitchAll)}}

                {{Indent(1, MatchAll)}}

                {{Indent(1, IsCase)}}
                {{Indent(1, Union.Cases.Select(c => $$"""

                {{Partial(c.Kind)}} {{c.Symbol}} : {{Union.Type.Symbol}}
                {
                    /// <summary>The discriminator value of {{DocSee(c)}}.</summary>
                    [{{CompilerGenerated}}]
                    [{{DebuggerBrowsable}}({{DebuggerBrowsableStateNever}})]
                    [{{EditorBrowsable}}({{EditorBrowsableStateNever}})]
                    public sealed override {{DiscriminatorType}} @{{DiscriminatorValue}} => {{CaseId(c)}};
                }
                """))}}
            }
            """;
    }
}