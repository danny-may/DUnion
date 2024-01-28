using DUnion;
using System.Collections.Generic;

namespace SubTyped.Valid.JsonValue;

[DUnion(Kind = UnionKind.SubType)]
public partial record class JsonValue
{
    public partial record class String(string Value);
    public partial record class Number(double Value);
    public partial record class Boolean(bool Value);
    public partial record class Null();
    public partial record class Array(IReadOnlyList<JsonValue> Values);
    public partial record class Object(IReadOnlyDictionary<string, JsonValue> Properties);
}