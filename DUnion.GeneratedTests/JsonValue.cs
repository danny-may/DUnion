using System.Collections.Generic;
using System.Reflection;

namespace DUnion;

[DUnion]
public readonly partial record struct JsonValue
{
    public readonly record struct String(string Value);
    public readonly record struct Number(double Value);
    public readonly record struct Boolean(bool Value);
    public readonly record struct Null();
    public readonly record struct Array(IReadOnlyList<JsonValue> Values);
    public readonly record struct Object(IReadOnlyDictionary<string, JsonValue> Properties);

    private static readonly FieldInfo _fDiscriminator = typeof(JsonValue).GetField(nameof(_discriminator), BindingFlags.NonPublic | BindingFlags.Instance)!;
    private static readonly FieldInfo _fValue = typeof(JsonValue).GetField(nameof(_value), BindingFlags.NonPublic | BindingFlags.Instance)!;

    public static JsonValue Invalid(byte discriminator, object? value)
    {
        object result = new JsonValue();
        _fDiscriminator.SetValue(result, discriminator);
        _fValue.SetValue(result, value);
        return (JsonValue)result;
    }

    public object? UnderlyingValue => _value;
}