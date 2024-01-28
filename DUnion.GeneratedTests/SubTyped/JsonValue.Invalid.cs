using DUnion;

namespace SubTyped.Valid.JsonValue;

public partial record JsonValue
{
    [DUnionExclude]
    public partial record class Invalid(byte State) : JsonValue
    {
        public override byte Discriminator => State;
    }
}