using FluentAssertions;
using System;
using System.Collections.Generic;
using Valid.JsonValue;

namespace DUnion;

public static class JsonValueTests
{
    private static readonly JsonValue[] _array = new JsonValue[] { new JsonValue.Number(123), new JsonValue.String("abc") };

    private static readonly IReadOnlyDictionary<string, JsonValue> _object = new Dictionary<string, JsonValue>
    {
        ["p1"] = new JsonValue.Number(123),
        ["p2"] = new JsonValue.String("abc")
    };

    public static TheoryData<JsonValue, JsonValue.Array?> ExplicitCast_FromJsonValue_ToArrayJsonValue_Data => new()
    {
        { new JsonValue.Null(), null },
        { new JsonValue.String("abc"), null },
        { new JsonValue.Number(1234), null },
        { new JsonValue.Boolean(true), null },
        { new JsonValue.Boolean(false), null },
        { new JsonValue.Array(_array), new(_array) },
        { new JsonValue.Object(_object), null }
    };

    public static TheoryData<JsonValue, JsonValue.Boolean?> ExplicitCast_FromJsonValue_ToBooleanJsonValue_Data => new()
    {
        { new JsonValue.Null(), null },
        { new JsonValue.String("abc"), null },
        { new JsonValue.Number(1234), null },
        { new JsonValue.Boolean(true), new(true) },
        { new JsonValue.Boolean(false), new(false) },
        { new JsonValue.Array(_array), null },
        { new JsonValue.Object(_object), null }
    };

    public static TheoryData<JsonValue, JsonValue.Null?> ExplicitCast_FromJsonValue_ToNullJsonValue_Data => new()
    {
        { new JsonValue.Null(), new() },
        { new JsonValue.String("abc"), null },
        { new JsonValue.Number(1234), null },
        { new JsonValue.Boolean(true), null },
        { new JsonValue.Boolean(false), null },
        { new JsonValue.Array(_array), null },
        { new JsonValue.Object(_object), null }
    };

    public static TheoryData<JsonValue, JsonValue.Number?> ExplicitCast_FromJsonValue_ToNumberJsonValue_Data => new()
    {
        { new JsonValue.Null(), null },
        { new JsonValue.String("abc"), null },
        { new JsonValue.Number(1234), new(1234) },
        { new JsonValue.Boolean(true), null },
        { new JsonValue.Boolean(false), null },
        { new JsonValue.Array(_array), null },
        { new JsonValue.Object(_object), null }
    };

    public static TheoryData<JsonValue, JsonValue.Object?> ExplicitCast_FromJsonValue_ToObjectJsonValue_Data => new()
    {
        { new JsonValue.Null(), null },
        { new JsonValue.String("abc"), null },
        { new JsonValue.Number(1234), null },
        { new JsonValue.Boolean(true), null },
        { new JsonValue.Boolean(false), null },
        { new JsonValue.Array(_array), null },
        { new JsonValue.Object(_object), new(_object) }
    };

    public static TheoryData<JsonValue, JsonValue.String?> ExplicitCast_FromJsonValue_ToStringJsonValue_Data => new()
    {
        { new JsonValue.Null(), null },
        { new JsonValue.String("abc"), new("abc") },
        { new JsonValue.Number(1234), null },
        { new JsonValue.Boolean(true), null },
        { new JsonValue.Boolean(false), null },
        { new JsonValue.Array(_array), null },
        { new JsonValue.Object(_object), null }
    };

    public static TheoryData<JsonValue.Array, JsonValue> ImplicitCast_ToJsonValue_FromArrayJsonValue_Data => new()
    {
        { new(_array), new JsonValue.Array(_array) },
    };

    public static TheoryData<JsonValue.Boolean, JsonValue> ImplicitCast_ToJsonValue_FromBooleanJsonValue_Data => new()
    {
        { new(true), new JsonValue.Boolean(true) },
        { new(false), new JsonValue.Boolean(false) },
    };

    public static TheoryData<JsonValue.Null, JsonValue> ImplicitCast_ToJsonValue_FromNullJsonValue_Data => new()
    {
        { new(), new JsonValue.Null() },
    };

    public static TheoryData<JsonValue.Number, JsonValue> ImplicitCast_ToJsonValue_FromNumberJsonValue_Data => new()
    {
        { new(1234), new JsonValue.Number(1234) },
    };

    public static TheoryData<JsonValue.Object, JsonValue> ImplicitCast_ToJsonValue_FromObjectJsonValue_Data => new()
    {
        { new(_object), new JsonValue.Object(_object) }
    };

    public static TheoryData<JsonValue.String, JsonValue> ImplicitCast_ToJsonValue_FromStringJsonValue_Data => new()
    {
        { new("abc"), new JsonValue.String("abc") },
    };

    public static TheoryData<JsonValue, bool, JsonValue.Array?> IsArray_Data => new()
    {
        { State(0), false, default },
        { new JsonValue.Null(), false, default },
        { new JsonValue.String("abc"), false, default },
        { new JsonValue.Number(123), false, default },
        { new JsonValue.Boolean(true), false, default },
        { new JsonValue.Boolean(false), false, default },
        { new JsonValue.Array(_array), true, new(_array) },
        { new JsonValue.Object(_object), false, default },
    };

    public static TheoryData<JsonValue, bool, JsonValue.Boolean?> IsBoolean_Data => new()
    {
        { State(0), false, default },
        { new JsonValue.Null(), false, default },
        { new JsonValue.String("abc"), false, default },
        { new JsonValue.Number(123), false, default },
        { new JsonValue.Boolean(true), true, new(true) },
        { new JsonValue.Boolean(false), true, new(false) },
        { new JsonValue.Array(_array), false, default },
        { new JsonValue.Object(_object), false, default },
    };

    public static TheoryData<JsonValue, bool, JsonValue.Null?> IsNull_Data => new()
    {
        { State(0), false, default },
        { new JsonValue.Null(), true, new() },
        { new JsonValue.String("abc"), false, default },
        { new JsonValue.Number(123), false, default },
        { new JsonValue.Boolean(true), false, default },
        { new JsonValue.Boolean(false), false, default },
        { new JsonValue.Array(_array), false, default },
        { new JsonValue.Object(_object), false, default },
    };

    public static TheoryData<JsonValue, bool, JsonValue.Number?> IsNumber_Data => new()
    {
        { State(0), false, default },
        { new JsonValue.Null(), false, default },
        { new JsonValue.String("abc"), false, default },
        { new JsonValue.Number(123), true, new(123) },
        { new JsonValue.Boolean(true), false, default },
        { new JsonValue.Boolean(false), false, default },
        { new JsonValue.Array(_array), false, default },
        { new JsonValue.Object(_object), false, default },
    };

    public static TheoryData<JsonValue, bool, JsonValue.Object?> IsObject_Data => new()
    {
        { State(0), false, default },
        { new JsonValue.Null(), false, default },
        { new JsonValue.String("abc"), false, default },
        { new JsonValue.Number(123), false, default },
        { new JsonValue.Boolean(true), false, default },
        { new JsonValue.Boolean(false), false, default },
        { new JsonValue.Array(_array), false, default },
        { new JsonValue.Object(_object), true, new(_object) },
    };

    public static TheoryData<JsonValue, bool, JsonValue.String?> IsString_Data => new()
    {
        { State(0), false, default },
        { new JsonValue.Null(), false, default },
        { new JsonValue.String("abc"), true, new("abc")},
        { new JsonValue.Number(123), false, default },
        { new JsonValue.Boolean(true), false, default },
        { new JsonValue.Boolean(false), false, default },
        { new JsonValue.Array(_array), false, default },
        { new JsonValue.Object(_object), false, default },
    };

    public static TheoryData<JsonValue, object?> Match_NoDefault_Data => new()
    {
        { new JsonValue.Null(), null },
        { new JsonValue.String("abc"), "abc" },
        { new JsonValue.Number(1234), 1234 },
        { new JsonValue.Boolean(true), true },
        { new JsonValue.Boolean(false), false },
        { new JsonValue.Array(_array), _array },
        { new JsonValue.Object(_object), _object },
    };

    public static TheoryData<JsonValue, string> Match_NoDefault_NullSelector_Data => new()
    {
        { new JsonValue.Null(), "caseNull" },
        { new JsonValue.String("abc"), "caseString" },
        { new JsonValue.Number(1234), "caseNumber" },
        { new JsonValue.Boolean(true), "caseBoolean" },
        { new JsonValue.Boolean(false), "caseBoolean" },
        { new JsonValue.Array(_array), "caseArray" },
        { new JsonValue.Object(_object), "caseObject" },
    };

    public static TheoryData<JsonValue> Match_WithDefault_Default_Data => new()
    {
        new JsonValue.Null(),
        new JsonValue.String("abc"),
        new JsonValue.Number(1234),
        new JsonValue.Boolean(true),
        new JsonValue.Boolean(false),
        new JsonValue.Array(_array),
        new JsonValue.Object(_object),
    };

    public static TheoryData<JsonValue, object?> Match_WithDefault_NotDefault_Data => new()
    {
        { new JsonValue.Null(), null },
        { new JsonValue.String("abc"), "abc" },
        { new JsonValue.Number(1234), 1234 },
        { new JsonValue.Boolean(true), true },
        { new JsonValue.Boolean(false), false },
        { new JsonValue.Array(_array), _array },
        { new JsonValue.Object(_object), _object },
    };

    public static TheoryData<JsonValue> Match_WithDefault_NullSelector_Default_Data => new()
    {
        new JsonValue.Null(),
        new JsonValue.String("abc"),
        new JsonValue.Number(1234),
        new JsonValue.Boolean(true),
        new JsonValue.Boolean(false),
        new JsonValue.Array(_array),
        new JsonValue.Object(_object),
    };

    public static TheoryData<JsonValue> Match_WithDefault_NullSelector_NullDefault_Data => new()
    {
        new JsonValue.Null(),
        new JsonValue.String("abc"),
        new JsonValue.Number(1234),
        new JsonValue.Boolean(true),
        new JsonValue.Boolean(false),
        new JsonValue.Array(_array),
        new JsonValue.Object(_object),
    };

    public static TheoryData<JsonValue, object?> Switch_NoDefault_Data => new()
    {
        { new JsonValue.Null(), null },
        { new JsonValue.String("abc"), "abc" },
        { new JsonValue.Number(1234), 1234 },
        { new JsonValue.Boolean(true), true },
        { new JsonValue.Boolean(false), false },
        { new JsonValue.Array(_array), _array },
        { new JsonValue.Object(_object), _object },
    };

    public static TheoryData<JsonValue, string> Switch_NoDefault_NullSelector_Data => new()
    {
        { new JsonValue.Null(), "caseNull" },
        { new JsonValue.String("abc"), "caseString" },
        { new JsonValue.Number(1234), "caseNumber" },
        { new JsonValue.Boolean(true), "caseBoolean" },
        { new JsonValue.Boolean(false), "caseBoolean" },
        { new JsonValue.Array(_array), "caseArray" },
        { new JsonValue.Object(_object), "caseObject" },
    };

    public static TheoryData<JsonValue> Switch_WithDefault_Default_Data => new()
    {
        new JsonValue.Null(),
        new JsonValue.String("abc"),
        new JsonValue.Number(1234),
        new JsonValue.Boolean(true),
        new JsonValue.Boolean(false),
        new JsonValue.Array(_array),
        new JsonValue.Object(_object),
    };

    public static TheoryData<JsonValue, object?> Switch_WithDefault_NotDefault_Data => new()
    {
        { new JsonValue.Null(), null },
        { new JsonValue.String("abc"), "abc" },
        { new JsonValue.Number(1234), 1234 },
        { new JsonValue.Boolean(true), true },
        { new JsonValue.Boolean(false), false },
        { new JsonValue.Array(_array), _array },
        { new JsonValue.Object(_object), _object },
    };

    public static TheoryData<JsonValue> Switch_WithDefault_NullSelector_Default_Data => new()
    {
        new JsonValue.Null(),
        new JsonValue.String("abc"),
        new JsonValue.Number(1234),
        new JsonValue.Boolean(true),
        new JsonValue.Boolean(false),
        new JsonValue.Array(_array),
        new JsonValue.Object(_object),
    };

    public static TheoryData<JsonValue> Switch_WithDefault_NullSelector_NullDefault_Data => new()
    {
        new JsonValue.Null(),
        new JsonValue.String("abc"),
        new JsonValue.Number(1234),
        new JsonValue.Boolean(true),
        new JsonValue.Boolean(false),
        new JsonValue.Array(_array),
        new JsonValue.Object(_object),
    };

    [Theory]
    [MemberData(nameof(ExplicitCast_FromJsonValue_ToArrayJsonValue_Data))]
    public static void ExplicitCast_FromJsonValue_ToArrayJsonValue(JsonValue value, JsonValue.Array? expected)
    {
        if (expected is null)
        {
            Assert.Throws<InvalidCastException>(() => (JsonValue.Array)value);
        }
        else
        {
            ((JsonValue.Array)value).Should().Be(expected);
        }
    }

    [Theory]
    [MemberData(nameof(ExplicitCast_FromJsonValue_ToBooleanJsonValue_Data))]
    public static void ExplicitCast_FromJsonValue_ToBooleanJsonValue(JsonValue value, JsonValue.Boolean? expected)
    {
        if (expected is null)
        {
            Assert.Throws<InvalidCastException>(() => (JsonValue.Boolean)value);
        }
        else
        {
            ((JsonValue.Boolean)value).Should().Be(expected);
        }
    }

    [Theory]
    [MemberData(nameof(ExplicitCast_FromJsonValue_ToNullJsonValue_Data))]
    public static void ExplicitCast_FromJsonValue_ToNullJsonValue(JsonValue value, JsonValue.Null? expected)
    {
        if (expected is null)
        {
            Assert.Throws<InvalidCastException>(() => (JsonValue.Null)value);
        }
        else
        {
            ((JsonValue.Null)value).Should().Be(expected);
        }
    }

    [Theory]
    [MemberData(nameof(ExplicitCast_FromJsonValue_ToNumberJsonValue_Data))]
    public static void ExplicitCast_FromJsonValue_ToNumberJsonValue(JsonValue value, JsonValue.Number? expected)
    {
        if (expected is null)
        {
            Assert.Throws<InvalidCastException>(() => (JsonValue.Number)value);
        }
        else
        {
            ((JsonValue.Number)value).Should().Be(expected);
        }
    }

    [Theory]
    [MemberData(nameof(ExplicitCast_FromJsonValue_ToObjectJsonValue_Data))]
    public static void ExplicitCast_FromJsonValue_ToObjectJsonValue(JsonValue value, JsonValue.Object? expected)
    {
        if (expected is null)
        {
            Assert.Throws<InvalidCastException>(() => (JsonValue.Object)value);
        }
        else
        {
            ((JsonValue.Object)value).Should().Be(expected);
        }
    }

    [Theory]
    [MemberData(nameof(ExplicitCast_FromJsonValue_ToStringJsonValue_Data))]
    public static void ExplicitCast_FromJsonValue_ToStringJsonValue(JsonValue value, JsonValue.String? expected)
    {
        if (expected is null)
        {
            Assert.Throws<InvalidCastException>(() => (JsonValue.String)value);
        }
        else
        {
            ((JsonValue.String)value).Should().Be(expected);
        }
    }

    [Theory]
    [MemberData(nameof(ImplicitCast_ToJsonValue_FromArrayJsonValue_Data))]
    public static void ImplicitCast_ToJsonValue_FromArrayJsonValue(JsonValue.Array value, JsonValue expected)
    {
        // arrange

        // act
        JsonValue actual = value;

        // assert
        actual.Should().Be(expected);
        actual.UnderlyingValue.Should().Be(value);
    }

    [Theory]
    [MemberData(nameof(ImplicitCast_ToJsonValue_FromBooleanJsonValue_Data))]
    public static void ImplicitCast_ToJsonValue_FromBooleanJsonValue(JsonValue.Boolean value, JsonValue expected)
    {
        // arrange

        // act
        JsonValue actual = value;

        // assert
        actual.Should().Be(expected);
        actual.UnderlyingValue.Should().Be(value);
    }

    [Theory]
    [MemberData(nameof(ImplicitCast_ToJsonValue_FromNullJsonValue_Data))]
    public static void ImplicitCast_ToJsonValue_FromNullJsonValue(JsonValue.Null value, JsonValue expected)
    {
        // arrange

        // act
        JsonValue actual = value;

        // assert
        actual.Should().Be(expected);
        actual.UnderlyingValue.Should().Be(value);
    }

    [Theory]
    [MemberData(nameof(ImplicitCast_ToJsonValue_FromNumberJsonValue_Data))]
    public static void ImplicitCast_ToJsonValue_FromNumberJsonValue(JsonValue.Number value, JsonValue expected)
    {
        // arrange

        // act
        JsonValue actual = value;

        // assert
        actual.Should().Be(expected);
        actual.UnderlyingValue.Should().Be(value);
    }

    [Theory]
    [MemberData(nameof(ImplicitCast_ToJsonValue_FromObjectJsonValue_Data))]
    public static void ImplicitCast_ToJsonValue_FromObjectJsonValue(JsonValue.Object value, JsonValue expected)
    {
        // arrange

        // act
        JsonValue actual = value;

        // assert
        actual.Should().Be(expected);
        actual.UnderlyingValue.Should().Be(value);
    }

    [Theory]
    [MemberData(nameof(ImplicitCast_ToJsonValue_FromStringJsonValue_Data))]
    public static void ImplicitCast_ToJsonValue_FromStringJsonValue(JsonValue.String value, JsonValue expected)
    {
        // arrange

        // act
        JsonValue actual = value;

        // assert
        actual.Should().Be(expected);
        actual.UnderlyingValue.Should().Be(value);
    }

    [Theory]
    [MemberData(nameof(IsArray_Data))]
    public static void IsArray(JsonValue value, bool expectedReturn, JsonValue.Array expectedOut)
    {
        // arrange

        // act
        var actualReturn = value.IsArray(out var actualOut);

        // assert
        actualReturn.Should().Be(expectedReturn);
        actualOut.Should().Be(expectedOut);
    }

    [Theory]
    [MemberData(nameof(IsBoolean_Data))]
    public static void IsBoolean(JsonValue value, bool expectedReturn, JsonValue.Boolean expectedOut)
    {
        // arrange

        // act
        var actualReturn = value.IsBoolean(out var actualOut);

        // assert
        actualReturn.Should().Be(expectedReturn);
        actualOut.Should().Be(expectedOut);
    }

    [Theory]
    [MemberData(nameof(IsNull_Data))]
    public static void IsNull(JsonValue value, bool expectedReturn, JsonValue.Null expectedOut)
    {
        // arrange

        // act
        var actualReturn = value.IsNull(out var actualOut);

        // assert
        actualReturn.Should().Be(expectedReturn);
        actualOut.Should().Be(expectedOut);
    }

    [Theory]
    [MemberData(nameof(IsNumber_Data))]
    public static void IsNumber(JsonValue value, bool expectedReturn, JsonValue.Number expectedOut)
    {
        // arrange

        // act
        var actualReturn = value.IsNumber(out var actualOut);

        // assert
        actualReturn.Should().Be(expectedReturn);
        actualOut.Should().Be(expectedOut);
    }

    [Theory]
    [MemberData(nameof(IsObject_Data))]
    public static void IsObject(JsonValue value, bool expectedReturn, JsonValue.Object expectedOut)
    {
        // arrange

        // act
        var actualReturn = value.IsObject(out var actualOut);

        // assert
        actualReturn.Should().Be(expectedReturn);
        actualOut.Should().Be(expectedOut);
    }

    [Theory]
    [MemberData(nameof(IsString_Data))]
    public static void IsString(JsonValue value, bool expectedReturn, JsonValue.String expectedOut)
    {
        // arrange

        // act
        var actualReturn = value.IsString(out var actualOut);

        // assert
        actualReturn.Should().Be(expectedReturn);
        actualOut.Should().Be(expectedOut);
    }

    [Theory]
    [MemberData(nameof(Match_NoDefault_Data))]
    public static void Match_NoDefault(JsonValue sut, object? expected)
    {
        // arrange

        // act
        var actual = sut.Match<object?>(
            caseString: v => v.Value,
            caseNumber: v => v.Value,
            caseBoolean: v => v.Value,
            caseNull: v => null,
            caseArray: v => v.Values,
            caseObject: v => v.Properties);

        // assert
        actual.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(Match_NoDefault_NullSelector_Data))]
    public static void Match_NoDefault_NullSelector(JsonValue sut, string parameter)
    {
        // arrange

        // act
        var actual = Assert.Throws<ArgumentNullException>(() => sut.Match<object?>(
            caseString: null!,
            caseNumber: null!,
            caseBoolean: null!,
            caseNull: null!,
            caseArray: null!,
            caseObject: null!));

        // assert
        actual.ParamName.Should().Be(parameter);
        actual.Message.Should().Be($"Value cannot be null. (Parameter '{parameter}')");
    }

    [Fact]
    public static void Match_NoDefault_Uninitialized()
    {
        // arrange
        var sut = State(0);

        // act
        var actual = Assert.Throws<InvalidOperationException>(() => sut.Match(
            caseString: MatchFail,
            caseNumber: MatchFail,
            caseBoolean: MatchFail,
            caseNull: MatchFail,
            caseArray: MatchFail,
            caseObject: MatchFail));

        // assert
        actual.Message.Should().Be("Union has not been initialized");
    }

    [Fact]
    public static void Match_NoDefault_Unsupported()
    {
        // arrange
        var sut = State(7);

        // act
        var actual = Assert.Throws<InvalidOperationException>(() => sut.Match(
            caseString: MatchFail,
            caseNumber: MatchFail,
            caseBoolean: MatchFail,
            caseNull: MatchFail,
            caseArray: MatchFail,
            caseObject: MatchFail));

        // assert
        actual.Message.Should().Be("Unsupported discriminator value 7");
    }

    [Theory]
    [MemberData(nameof(Match_WithDefault_Default_Data))]
    public static void Match_WithDefault_Default(JsonValue sut)
    {
        // arrange
        var expected = Guid.NewGuid();

        // act
        var actual = sut.Match<object?>(
            @default: () => expected);

        // assert
        actual.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(Match_WithDefault_NotDefault_Data))]
    public static void Match_WithDefault_NotDefault(JsonValue sut, object? expected)
    {
        // arrange

        // act
        var actual = sut.Match(
            @default: MatchFail,
            caseString: v => v.Value,
            caseNumber: v => v.Value,
            caseBoolean: v => v.Value,
            caseNull: v => null,
            caseArray: v => v.Values,
            caseObject: v => v.Properties);

        // assert
        actual.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(Match_WithDefault_NullSelector_Default_Data))]
    public static void Match_WithDefault_NullSelector_Default(JsonValue sut)
    {
        // arrange
        var expected = Guid.NewGuid();

        // act
        var actual = sut.Match<object?>(@default: () => expected);

        // assert
        actual.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(Match_WithDefault_NullSelector_NullDefault_Data))]
    public static void Match_WithDefault_NullSelector_NullDefault(JsonValue sut)
    {
        // arrange

        // act
        var actual = Assert.Throws<ArgumentNullException>(() => sut.Match<object?>(@default: null!));

        // assert
        actual.ParamName.Should().Be("default");
        actual.Message.Should().Be($"Value cannot be null. (Parameter 'default')");
    }

    [Fact]
    public static void Match_WithDefault_Uninitialized()
    {
        // arrange
        var sut = State(0);

        // act
        var actual = Assert.Throws<InvalidOperationException>(() => sut.Match(@default: MatchFail));

        // assert
        actual.Message.Should().Be("Union has not been initialized");
    }

    [Fact]
    public static void Match_WithDefault_Unsupported()
    {
        // arrange
        var sut = State(7);

        // act
        var actual = Assert.Throws<InvalidOperationException>(() => sut.Match(@default: MatchFail));

        // assert
        actual.Message.Should().Be("Unsupported discriminator value 7");
    }

    [Theory]
    [MemberData(nameof(Switch_NoDefault_Data))]
    public static void Switch_NoDefault(JsonValue sut, object? expected)
    {
        // arrange
        var actual = null as V<object?>?;

        // act
        sut.Switch(
            caseString: v => actual = new(v.Value),
            caseNumber: v => actual = new(v.Value),
            caseBoolean: v => actual = new(v.Value),
            caseNull: v => actual = new(null),
            caseArray: v => actual = new(v.Values),
            caseObject: v => actual = new(v.Properties));

        // assert
        actual.Should().NotBeNull();
        actual!.Value.Value.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(Switch_NoDefault_NullSelector_Data))]
    public static void Switch_NoDefault_NullSelector(JsonValue sut, string parameter)
    {
        // arrange

        // act
        var actual = Assert.Throws<ArgumentNullException>(() => sut.Switch(
            caseString: null!,
            caseNumber: null!,
            caseBoolean: null!,
            caseNull: null!,
            caseArray: null!,
            caseObject: null!));

        // assert
        actual.ParamName.Should().Be(parameter);
        actual.Message.Should().Be($"Value cannot be null. (Parameter '{parameter}')");
    }

    [Fact]
    public static void Switch_NoDefault_Uninitialized()
    {
        // arrange
        var sut = State(0);

        // act
        var actual = Assert.Throws<InvalidOperationException>(() => sut.Switch(
            caseString: SwitchFail,
            caseNumber: SwitchFail,
            caseBoolean: SwitchFail,
            caseNull: SwitchFail,
            caseArray: SwitchFail,
            caseObject: SwitchFail));

        // assert
        actual.Message.Should().Be("Union has not been initialized");
    }

    [Fact]
    public static void Switch_NoDefault_Unsupported()
    {
        // arrange
        var sut = State(7);

        // act
        var actual = Assert.Throws<InvalidOperationException>(() => sut.Switch(
            caseString: SwitchFail,
            caseNumber: SwitchFail,
            caseBoolean: SwitchFail,
            caseNull: SwitchFail,
            caseArray: SwitchFail,
            caseObject: SwitchFail));

        // assert
        actual.Message.Should().Be("Unsupported discriminator value 7");
    }

    [Theory]
    [MemberData(nameof(Switch_WithDefault_Default_Data))]
    public static void Switch_WithDefault_Default(JsonValue sut)
    {
        // arrange
        var expected = Guid.NewGuid();
        var actual = null as V<object?>?;

        // act
        sut.Switch(
            @default: () => actual = new(expected));

        // assert
        actual.Should().NotBeNull();
        actual!.Value.Value.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(Switch_WithDefault_NotDefault_Data))]
    public static void Switch_WithDefault_NotDefault(JsonValue sut, object? expected)
    {
        // arrange
        var actual = null as V<object?>?;

        // act
        sut.Switch(
            @default: SwitchFail,
            caseString: v => actual = new(v.Value),
            caseNumber: v => actual = new(v.Value),
            caseBoolean: v => actual = new(v.Value),
            caseNull: v => actual = new(null),
            caseArray: v => actual = new(v.Values),
            caseObject: v => actual = new(v.Properties));

        // assert
        actual.Should().NotBeNull();
        actual!.Value.Value.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(Switch_WithDefault_NullSelector_Default_Data))]
    public static void Switch_WithDefault_NullSelector_Default(JsonValue sut)
    {
        // arrange
        var expected = Guid.NewGuid();
        var actual = null as V<object?>?;

        // act
        sut.Switch(@default: () => actual = new(expected));

        // assert
        actual.Should().NotBeNull();
        actual!.Value.Value.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(Switch_WithDefault_NullSelector_NullDefault_Data))]
    public static void Switch_WithDefault_NullSelector_NullDefault(JsonValue sut)
    {
        // arrange

        // act
        var actual = Assert.Throws<ArgumentNullException>(() => sut.Switch(@default: null!));

        // assert
        actual.ParamName.Should().Be("default");
        actual.Message.Should().Be($"Value cannot be null. (Parameter 'default')");
    }

    [Fact]
    public static void Switch_WithDefault_Uninitialized()
    {
        // arrange
        var sut = State(0);

        // act
        var actual = Assert.Throws<InvalidOperationException>(() => sut.Switch(@default: SwitchFail));

        // assert
        actual.Message.Should().Be("Union has not been initialized");
    }

    [Fact]
    public static void Switch_WithDefault_Unsupported()
    {
        // arrange
        var sut = State(7);

        // act
        var actual = Assert.Throws<InvalidOperationException>(() => sut.Switch(@default: SwitchFail));

        // assert
        actual.Message.Should().Be("Unsupported discriminator value 7");
    }

    private static object? MatchFail<T>(T _)
    {
        throw new Exception("Match shouldnt have worked.");
    }

    private static object? MatchFail()
    {
        throw new Exception("Match shouldnt have worked.");
    }

    private static JsonValue State(byte state)
    {
        return JsonValue.Invalid(state, null);
    }

    private readonly record struct V<T>(T Value);

    private static void SwitchFail<T>(T _)
    {
        throw new Exception("Switch shouldnt have worked.");
    }

    private static void SwitchFail()
    {
        throw new Exception("Switch shouldnt have worked.");
    }
}