# DUnion

Easy source generator for creating custom discriminated unions.

# Define a new discriminated union

```csharp
[DUnion]
public static class CreateUserResult
{
    public readonly record struct Success(User User);
    public readonly record struct NameInUse(Guid UserId);
    public readonly record struct NameTooLong(int MaxLength);
    public readonly record struct NameTooShort(int MinLength);
}
```

# Use the generated discriminated union

```csharp
public CreateUserResult CreateUser(User user) 
{
    if (user.Name is null or { Length: < 5 }) 
    {
        return new CreateUserResult.NameTooShort(5);
    }
    
    if (user.Name.Length > 30) 
    {
        return new CreateUserResult.NameTooLong(30);
    }

    var existingUser = userStore.FindByName(user.Name);
    if (existingUser is not null)
    {
        return new CreateUserResult.NameInUse(existingUser.Id);
    }

    var createdUser = userStore.Add(user);
    return new CreateUserResult.Success(createdUser);
}

public IActionResult HandleSignUpRequest(User user) 
{
    return CreateUser(user)
        .Match(
            caseSuccess: success => Ok(success.User.Id),
            caseNameInUse: error => BadRequest($"The name is already in use by user {error.UserId}."),
            @default: () => BadRequest()
        )
}
```

# Supports generics

```csharp
// Looks like rust! https://doc.rust-lang.org/std/option/
[DUnion]
public static class Option
{
    public readonly record struct Some<T>(T Value);
    public readonly record struct None();
}

public Option<double> Divide(double numerator, double denominator) 
{
    if (denominator == 0)
    {
        return new Option.None();
    }
    else
    {
        return new Option.Some<double>(numerator / denominator);
    }
}

Divide(2.0, 3.0)
    .Switch(
        caseSome: some => Console.WriteLine($"Result: {some.Value}"),
        caseNone: none => Console.WriteLine("Cannot divide by 0")
    );
```

## Multiple generics are also supported

By default, type parameters with the same name across cases will be merged into the same type parameter on the union, but you can customize this by using the `[DUnionGeneric]` attribute.  

```csharp
// Also looks a bit like rust! https://doc.rust-lang.org/std/result/
[DUnion]
public static class Result
{
    public readonly record struct Ok<[DUnionGeneric("TOk")]T>(T Value);
    public readonly record struct Err<[DUnionGeneric("TErr")]T>(T Error);
}

public enum Version 
{
    Version1,
    Version2
}

public Result<Version, string> ParseVersion(int[] header) 
{
    switch (header) 
    {
        case []: return new Result.Err<string>("Invalid header length");
        case [1]: return new Result.Ok<Version>(Version.Version1);
        case [2]: return new Result.Ok<Version>(Version.Version2);
        default: return new Result.Err<string>("Invalid version");
    }
}

ParseVersion([1, 2, 3, 4])
    .Switch(
        caseOk: ok => Console.WriteLine($"Working with version: {ok.Value}"),
        caseErr: err => Console.WriteLine($"Error parsing header: {err.Error}")
    );
```

# Unions can be extended

You can add methods onto the union type itself to add custom helper methods, making using the union easier.

```csharp
public readonly record struct JsonValue
{
    public readonly record struct String(string Value);
    public readonly record struct Number(double Value);
    public readonly record struct Boolean(bool Value);
    public readonly record struct Null();
    public readonly record struct Array(IReadOnlyList<JsonValue> Values);
    public readonly record struct Object(IReadOnlyDictionary<string, JsonValue> Properties);

    public string Stringify() 
    {
        return Match(
            caseString: str => Escape(str.Value),
            caseBoolean: bool => bool.Value.ToString(),
            caseNull: _ => "null",
            caseArray: arr => $"[{string.Join(",", arr.Values.Select(v => v.Stringify()))}]",
            caseObject: obj => $"{{{string.Join(",", obj.Properties.Select(p => $"{Escape(p.Key)}: {p.Value.Stringify()}"))}}}"
        )
    }

    private static string Escape(string value) 
    {
        return "\"...\"";
    }
}
```

# Using in multiple projects

In most situations, you will be fine to add this source generator to any of your projects, however it does come with a bit of duplication if you do so. Each place where you add this package will have a set of internal attributes added, namely `DUnion.DUnionAttribute`, `DUnion.DUnionCaseAttribute`, `DUnion.DUnionGenericAttribute`, and `DUnion.DUnionExcludeAttribute`. These might therefore be duplicated many times and slightly inflate your build output. Theres also an issue with the `[InternalsVisibleTo]` attribute. If two projects have the source generator installed, and one has its internals visible to the other, then the build will fail due to ambiguous references to the attributes.

To solve all these issues, you can install the `DUnion.Attributes` package, and add the following to your `.csproj` files:

```xml
<PropertyGroup>
    <DefineConstants>DUNION_OMIT_ATTRIBUTES</DefineConstants>
</PropertyGroup>
```

Everything should work just fine after that!

# Union members

The generated unions have some members defined on them to allow you to interact with the case they wrap:

## Constructors

The union type will automatically contain a constructor for each of the cases it encompasses. These constructors can be used to wrap a case so that it can be used wherever the union type is required. Typically you wont need to use the constructors as there are implicit conversions from the cases to the union, but theyre there nonetheless!

- `public MyUnion({Case} value)`

## Switch

The `Switch` method is intended to mimic the c# `switch` statement, meaning you supply a number of handlers for each case you wish to accept, and optionally supply a default case, which can be null. To help reduce situations where a newly added case is missed, there are two overloads of the `Switch` method:

- The `Switch` method with a `@default` parameter has all other parameters marked as optional. This is useful if you only ever want to handle some of the cases, and are confident that you will not need to handle any potential future ones.

- The `Switch` method without a `@default` parameter requires that all possible cases have an argument supplied, although you are allowed to supply `null` if you do not wish to handle a specific case. This overload is useful for when you want to ensure that any future cases that may be added to the union are not forgotten.

```csharp
[DUnion]
public readonly record struct AccountType
{
    public readonly record struct User(string Email);
    public readonly record struct Admin(string Email);
    public readonly record struct Service(string Name, AccountType Owner);
    public readonly record struct System(Guid Id);
}

AccountType account = GetAccountType(id);


account.Switch(
    caseUser: user => { ... }, // Called if the account is of type AccountType.User
    caseAdmin: user => { ... }, // Called if the account is of type AccountType.Admin
    caseService: null // accounts of type AccountType.Service are ignored
    // Error: value for caseSystem is not supplied
)

account.Switch(
    caseUser: user => { ... }, // Called if the account is of type AccountType.User
    caseAdmin: user => { ... }, // Called if the account is of type AccountType.Admin
    caseService: null, // accounts of type AccountType.Service are ignored
    @default: null // accounts of type AccountType.System and any other ones added in the future are ignored
)
```

The name of the `Switch` method can be changed by setting the `SwitchName` property on the `[DUnion]` attribute.

```csharp
[DUnion(SwitchName = "MyPreferredSwitchName")]
public readonly record struct MyUnion
{
    ...
}
```

## Match

The `Match` method is intended to mimic the c# `switch` expression, meaning you supply a number of handlers for each case you wish to accept, and optionally supply a default case. One of these handlers will be called, and its result will be returned. To help reduce situations where a newly added case is missed, there are two overloads of the `Match` method:

- The `Match` method with a `@default` parameter has all other parameters marked as optional. This is useful if you only ever want to handle some of the cases, and are confident that you will not need to handle any potential future ones.

- The `Match` method without a `@default` parameter requires that all possible cases have an argument supplied. This overload is useful for when you want to ensure that any future cases that may be added to the union are not forgotten.

```csharp
[DUnion]
public readonly record struct AccountType
{
    public readonly record struct User(string Email);
    public readonly record struct Admin(string Email);
    public readonly record struct Service(string Name, AccountType Owner);
    public readonly record struct System(Guid Id);
}

AccountType account = GetAccountType(id);

account.Match(
    caseUser: user => { return ... }, // Called if the account is of type AccountType.User
    caseAdmin: user => { return ... }, // Called if the account is of type AccountType.Admin
    caseService: null // Error: ArgumentNullException
    // Error: value for caseSystem is not supplied
)

account.Match(
    caseUser: user => { return ... }, // Called if the account is of type AccountType.User
    caseAdmin: user => { return ... }, // Called if the account is of type AccountType.Admin
    @default: () => { return ... } // accounts of type AccountType.System, AccountType.Service and any other ones added in the future will cause the default to be called
)
```

The name of the `Match` method can be changed by setting the `MatchName` property on the `[DUnion]` attribute.

```csharp
[DUnion(MatchName = "MyPreferredMatchName")]
public readonly record struct MyUnion
{
    ...
}
```

## Is{Case}

The `Is{Case}` method is intended to mimic the `x is Case value` expression. It returns `true` and sets `out value` to the value of the case if the current union represents the specified case; otherwise `false` and `default(Case)` will be used.

```csharp
[DUnion]
public static class Option
{
    public readonly record struct Some<T>(T Value);
    public readonly record struct None();
}

Option<int> result = GetResult();
if (result.IsSome(out var some)) 
{
    // Do something with `some`
}

if (result.IsNone(out var none))
{
    // Do something with `none`
}
```

The name of the `Is{Case}` method can be changed by setting the `IsCaseName` property on the `[DUnionCase]` attribute.

```csharp
[DUnion]
public readonly record struct MyUnion
{
    [DUnionCase(IsCaseName = "IsSuccess")]
    public readonly record struct Ok();
}
```

## As{Case}OrDefault

The `As{Case}OrDefault` method is intended to mimic the `x as Case` expression. It returns the value of the case if the union represents the specified case type; otherwise a default value will be returned.

There are three overloads for `As{Case}OrDefault`, allowing you to specify what should be used as the `default` value in the case.

- No arguments will use `default(Case)` as the default return value.
- A `Func<Case>` argument will use the result of invoking the delegate as the default return value.
- A `Case` argument will use that value as the default return value.

```csharp
[DUnion]
public static class Option
{
    public record Some<T>(T Value);
    public record None();
}

Option<int> result = GetResult();
var nullOrSome = result.AsSomeOrDefault();
var alwaysSome = result.AsSomeOrDefault(new Option.Some<T>(0));
var alsoAlwaysSome = result.AsSomeOrDefault(() => new Option.Some<T>(0));

var nullOrNone = result.AsNoneOrDefault();
var alwaysNone = result.AsNoneOrDefault(new Option.None());
var alsoAlwaysNone = result.AsNoneOrDefault(() => new Option.None());
```
The name of the `As{Case}OrDefault` method can be changed by setting the `AsCaseOrDefault` property on the `[DUnionCase]` attribute.

```csharp
[DUnion]
public readonly record struct MyUnion
{
    [DUnionCase(AsCaseOrDefault = "AsSuccessOrDefault")]
    public readonly record struct Ok();
}
```

## IEquatable<Union>

All unions are equatable to themselves. For two unions to be considered equal, both the type of their case, and the value of their case must be equal. The following methods are implemented which relate to this:

- `static bool Equals(MyUnion left, MyUnion right)`
- `bool Equals(MyUnion other)`
- `bool IEquatable<MyUnion>.Equals(MyUnion other)`
- `override bool Equals(object? other)`
- `static bool operator ==(MyUnion left, MyUnion right)`
- `static bool operator !=(MyUnion left, MyUnion right)`
- `override int GetHashCode()`

## Casting

Unions can also be converted to their cases via casting, and vice versa. Going from a case to a union is an implicit cast, while going from a union to a case is explicit and may throw an exception if the cast is not valid. The following methods are implemented which relate to this:

- `static implicit operator MyUnion({Case} value)`
- `static explicit operator {Case}(MyUnion value)`

NOTE: Conversions to and from an interface cannot be defined, so if a case is an interface you cannot cast between it and the union, and vice versa.


## Fields

There are two `private readonly` fields located on the union instances. These fields generally should not be used for anything, but you can expose them through some readonly properties if you wish. I would advise not attepting to set these values yourself, for reasons detailed below.

### `byte _discriminator`

This holds a number indicating which type of case the union is currently wrapping. The mapping from the value of this field to the case type is not stored anywhere, so you should not rely on the value of this. If the order in which you define the cases changes, the meaning of the values of this field will also change. This field may also be a `ushort` instead of a `byte` if there are more than 254 cases.

The only value whos meaning is built in is `0`. A value of 0 means this union instance has not been constructed properly. Ordinarily this can only happen if the union is a `struct` and is the default value.

```csharp
[DUnion]
public readonly struct class Option
{
    public readonly record struct Some(string Value);
    public readonly record struct None();

    public byte Discriminator => _discriminator;
}

Option value = default;
value.Discriminator == 0; // true
```

The name of the `_discriminator` field can be changed by setting the `DiscriminatorName` property on the `[DUnion]` attribute. This is mainly to allow mitigation of potential name collisions.

```csharp
[DUnion(DiscriminatorName = "_someOtherDiscriminatorName")]
public readonly record struct MyUnion
{
    ...
}
```

### `object? _value`

This holds the current case the union is wrapping. You can access this value if you want to be able to access the type in a non-type-safe way. It will be your responsibility to perform any type checks on this before casting.

The name of the `_value` field can be changed by setting the `ValueName` property on the `[DUnion]` attribute. This is mainly to allow mitigation of potential name collisions.

```csharp
[DUnion(ValueName = "_someOtherValueName")]
public readonly record struct MyUnion
{
    ...
}
```

# Type safety

Due to the way c# works, under the hood all cases are stored in the [`object? _value`](#object-_value) field in the union. Converting to or from a strongly typed case to `object?` takes time, especially if the case is a value type like a `struct` or `enum`. To squeeze as much speed out of the union, there is an opt-in way to leverage the `System.Runtime.CompilerServices.Unsafe` class. This class allows us to skip a lot of the "slow" type checks when converting from `object?` to the strongly typed cases. Under normal usage of the unions, this is a safe process as all types are strongly checked before writing to the [`_value`](#object-_value) field. This means it is safe to enable the usage of the `Unsafe` class in almost all situations.

`UseUnsafe` is turned off by default simply to reduce the chance of things being broken without realising, as it effectively turns off some normally unneeded checks. If you are having performance issues, and have identified that this will help alleviate them, and you have checked it is safe to do so, then feel free to turn this feature on at a per-union level.

If, however, any method is used to modify or set the [`_value`](#object-_value) or [`_discriminator`](#byte-_discriminator) fields yourself, then you must also maintain these checks yourself to ensure that the values at runtime are correctly set. 


```csharp
[DUnion(UseUnsafe = true)]
public static class Option
{
    public record Some<T>(T Value);
    public record None();
}

public partial class Option<T> 
{
    public Option(T value)
    {
        // Dangerous: UseUnsafe is enabled, but the values of _value and _discriminator might not align any more! If the order of Some and None got swapped, this would be incorrect.
        this._value = new Option.Some<T>(value);
        this._discriminator = 1; 
    }

    public Option(T value) : this(new Option.Some<T>(value)) 
    {
        // Safe: Setting of _value and _discriminator is delegated to the source generated code, so their relationship will be maintained.
    }
}

Option<int> union = new Option.Some<int>(123);
// Dangerous: UseUnsafe is enabled, but the values of _value and _discriminator might not align any more! If the order of Some and None got swapped, this would be incorrect.
typeof(Option<int>)
    .GetField("_value", BindingFlags.NonPublic | BindingFlags.Instance)!
    .SetValue(new Option.None());
typeof(Option<int>)
    .GetField("_discriminator", BindingFlags.NonPublic | BindingFlags.Instance)!
    .SetValue(2);

```