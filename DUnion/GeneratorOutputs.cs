using DUnion.Models;
using Microsoft.CodeAnalysis;
using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using static DUnion.Models.Constants;

namespace DUnion;

internal static class GeneratorOutputs
{
    private static readonly Assembly _assembly = typeof(GeneratorOutputs).Assembly;

    private static readonly Regex _public = new("public", RegexOptions.Compiled);

    public static void ExcludeNonUnionCases(IncrementalGeneratorInitializationContext context)
    {
        Report(context, context.SyntaxProvider.ForAttributeWithMetadataName(
            DUnionCaseExcludeAttribute,
            static (_, _) => true,
            static (ctx, token) =>
            {
                var report = new GeneratorContext(ctx.SemanticModel, token);

                if (!Helpers.IsUnionCase(ctx.TargetSymbol))
                    report.AttributeOnlyOnCase(DUnionCaseExcludeAttribute, ctx.Attributes);

                return report;
            }));
    }

    public static void ExplicitNonUnionCases(IncrementalGeneratorInitializationContext context)
    {
        Report(context, context.SyntaxProvider.ForAttributeWithMetadataName(
            DUnionCaseAttribute,
            static (_, _) => true,
            static (ctx, token) =>
            {
                var report = new GeneratorContext(ctx.SemanticModel, token);

                if (!Helpers.IsUnionCase(ctx.TargetSymbol))
                    report.AttributeOnlyOnCase(DUnionCaseAttribute, ctx.Attributes);

                return report;
            }));
    }

    public static void GenericNonUnionCaseTypeParameters(IncrementalGeneratorInitializationContext context)
    {
        Report(context, context.SyntaxProvider.ForAttributeWithMetadataName(
            DUnionGenericAttribute,
            static (_, _) => true,
            static (ctx, token) =>
            {
                var report = new GeneratorContext(ctx.SemanticModel, token);

                if (!IsValid(ctx.TargetSymbol))
                    report.AttributeOnlyOnCaseTypeParameter(DUnionGenericAttribute, ctx.Attributes);

                return report;
            }));

        static bool IsValid(ISymbol symbol)
        {
            return symbol is ITypeParameterSymbol { DeclaringType: { } declaration }
                && Helpers.IsUnionCase(declaration);
        }
    }

    public static void StaticFiles(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static context =>
        {
            foreach (var name in _assembly.GetManifestResourceNames())
            {
                if (name.EndsWith(".cs"))
                {
                    using var sr = new StreamReader(_assembly.GetManifestResourceStream(name));
                    var source = sr.ReadToEnd();
                    context.AddSource(name, Helpers.ToSourceText(_public.Replace(source, "internal", 1)));
                }
            }
        });
    }

    public static void Unions(IncrementalGeneratorInitializationContext context)
    {
        var unions = context.SyntaxProvider.ForAttributeWithMetadataName(DUnionAttribute, static (_, _) => true, ParseUnionAttribute);
        context.RegisterSourceOutput(unions, ProduceOutputs);
    }

    private readonly record struct UnionState(Union? Model, GeneratorContext Context, Models.Location Location);

    private static UnionState ParseUnionAttribute(GeneratorAttributeSyntaxContext context, CancellationToken token)
    {
        var ctx = new GeneratorContext(context.SemanticModel, token);
        var location = context.TargetNode.GetLocation();
        try
        {
            var union = UnionParser.Parse(context.TargetSymbol, context.SemanticModel, [.. context.Attributes], ctx);
            return new(union, ctx, location);
        }
        catch (Exception ex)
        {
            ctx.InternalError([location], ex);
            return new(null, ctx, location);
        }
    }

    private static void ProduceOutputs(SourceProductionContext context, UnionState state)
    {
        try
        {
            if (state.Model is { } model)
            {
                var fileName = Helpers.FileName(model.Id) + ".g.cs";
                context.AddSource(fileName, Helpers.ToSourceText(UnionTemplate.Render(model)));
            }

            state.Context.SendDiagnostics(context);
        }
        catch (Exception ex)
        {
            var ctx = new GeneratorContext(null!, context.CancellationToken);
            ctx.InternalError([state.Location], ex);
            ctx.SendDiagnostics(context);
        }
    }

    private static void Report(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<GeneratorContext> reporter)
    {
        context.RegisterSourceOutput(reporter, static (ctx, reporter) => reporter.SendDiagnostics(ctx));
    }
}