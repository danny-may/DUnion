using DUnion.Generators;
using DUnion.Models;
using DUnion.Readers;
using Microsoft.CodeAnalysis;
using System;
using System.Reflection;

namespace DUnion;

[Generator]
internal sealed class SourceGenerator : IIncrementalGenerator
{
    private static readonly Assembly _assembly = typeof(SourceGenerator).Assembly;
    private readonly IUnionReader _reader;
    private readonly IStaticUnionGenerator? _staticGenerator;

    public SourceGenerator() : this(Defaults.Reader, Defaults.StaticGenerator)
    {
    }

    public SourceGenerator(IUnionReader reader, IStaticUnionGenerator? staticGenerator)
    {
        _reader = reader;
        _staticGenerator = staticGenerator;
    }

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => _staticGenerator?.Execute(ctx));

        var generator = context.SyntaxProvider.ForAttributeWithMetadataName(
            Constants.DUnionFullName,
            static delegate { return true; },
            (context, _) =>
            {
                try
                {
                    return _reader.Read(context);
                }
                catch (Exception ex)
                {
                    return new EmptyUnionGenerator(context.TargetNode.GetLocation())
                        .WithDiagnostics([Diagnostics.InternalError(context.TargetNode.GetLocation(), ex)]);
                }
            });

        context.RegisterSourceOutput(generator, static (ctx, generator) =>
        {
            try
            {
                generator.Execute(ctx);
            }
            catch (Exception ex)
            {
                ctx.ReportDiagnostic(Diagnostics.InternalError(generator.Location, ex));
            }
        });
    }
}