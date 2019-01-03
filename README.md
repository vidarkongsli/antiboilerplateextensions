# Anti-boilerplate

Remove boilerplate from your C# code.

[![Build Status](https://dev.azure.com/vidarkongsli/vidarkongsli/_apis/build/status/vidarkongsli.antiboilerplateextensions?branchName=master)](https://dev.azure.com/vidarkongsli/vidarkongsli/_build/latest?definitionId=1?branchName=master)

## Installation

```bash
dotnet add package antiboilerplate --version 1.1.0
```

## Functional

Sprinkle some    functional magic on your C# code

### Map

Map your object of type `TIn` to type `TOut` using the provided mapping function:

```csharp
var httpRequest = someObject
    .Map(o => JsonConvert.SerializeObject(o))
    .Map(o => new StringContent(o, Encoding.UTF8, "application/json"))
    .Map(o => new HttpRequestMessage(HttpMethod.Post,"chat.postMessage")
        {
            Content = o
        });
```

### Then

Perform action on object before returning it.

```csharp
public string CreateMessage(string data)
    => $"Some message: {data}".Then(s => Console.WriteLine(s));
```

### AsCollection

Treat any object as a collection (typically to be able to use Linq).

```csharp
var transformedArray = someArray
    .Union(someObject.AsCollection())
    .Select(x => doAMapping(x));
```

### Each

Perform an action on each element in an array (similar to `foreach`).

```csharp
new []{1, 2, 3}
    .Select(x => x * x)
    .Each(Console.WriteLine); // method group
```

## Array

Deconstruct arrays (C# 7).

```csharp
var (lastName, firstName, _) =
    "Chaplin, Charlie Spencer".Split(new[] {' ', ','},
        StringSplitOptions.RemoveEmptyEntries);
```

## String

### IsNullOrEmpty

```csharp
if (someString.IsNullOrEmpty())
{
    ...
}
```

### IsNullOrWhitespace

```csharp
if (someString.IsNullOrWhitespace())
{
    ...
}
```

### ToUtf8Base64

Example: creating a basic authorization header.

```csharp
var authorizationHeader = "Basic "
   + $"{username}:{password}".ToUtf8Base64()
```

### FromUtf8Base64

Example: decoding a basic authorization header. (Including array deconstruction)

```csharp
    var (username, password) = authorizationHeader
        .FromUtf8Base64()
        .Split(':');
```

## Disposables

Handle disposable objects without using unnecessary variables.

```csharp
var text = Disposable.Using(() => File.OpenText("somefile.txt"), file => file.ReadToEnd());
```

Instead of:

```csharp
string text;
using (var file = File.OpenText("somefile.txt"))
{
    text = file.ReadToEnd();
}
```