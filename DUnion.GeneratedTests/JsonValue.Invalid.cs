namespace Valid.JsonValue;
public partial record struct JsonValue
{
    public static JsonValue Invalid(byte discriminator, object? value)
    {
        return new(discriminator, value);
    }
}