# Anti-boilerplate

Remove boilerplate from your C# code.

[![Build Status](https://dev.azure.com/vidarkongsli/vidarkongsli/_apis/build/status/vidarkongsli.antiboilerplateextensions?branchName=master)](https://dev.azure.com/vidarkongsli/vidarkongsli/_build/latest?definitionId=1?branchName=master)

## Installation

```bash
dotnet add package antiboilerplate --version 1.4.3
```

## Functional

Sprinkle some    functional magic on your C# code

### Map

Map your object of type `TIn` to type `TOut` using the provided mapping function:

```csharp
var httpRequest = someObject
    .Map(JsonSerializer.Serialize) // method group
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
    => $"Some message: {data}".Then(Console.WriteLine);
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

## Enumerables

Turn a list of key-value pairs into a dictionary (where keys are not unique)

```csharp
var dictionary = new List<KeyValuePair<string, string>>
{
    new("a", "1"),
    new("b", "2"),
    new("a", "3")
}.ToDictionaryOfArrays(); // IDictionary<string, string[]>

var a = dictionary["a"]; // -> ["1", "3"]
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

### HasText

```csharp
if (someString.HasText()) // Inverse of .IsNullOrWhitespace
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

## Embedded resources

Read an embedded resource:

```csharp
var data = await EmbeddedResource.Read("Antiboilerplate.tests.TestData.ConfigurationTestData.json",
    Assembly.GetExecutingAssembly());
```

Read a Json file by naming convention. Add a marker type in with the same name and in the same namespace as the embedded resource:

```csharp
// Declare type in the same namespace as ConfigurationTestData.json
internal class ConfigurationTestData {}
...
string data = await EmbeddedResource.ReadJson<ConfigurationTestData>();
```

As above, but parse the Json file using `DataContractJsonSerializer`:

```csharp
ConfigurationTestContents config = await EmbeddedResource.ReadAndDeserializeJson<ConfigurationTestData, ConfigurationTestContents>();
```

Reads and parses and XML file:

```csharp
XDocument data = await EmbeddedResource.ReadXml<XmlTestData>();
```

## URL

Parse the query from a URI:

```csharp
IDictionary<string,string[]> query = new Uri("http://host/path?a=1&b=2")
    .ParseQueryString()
    .ToDictionaryOfArrays();

var a = query["a"].Single(); // => 1
```

Parse the query from a string:

```csharp
IDictionary<string,string[]> query = "http://host/path?a=1&b=2&a=3"
    .ParseQueryString()
    .ToDictionaryOfArrays();
    
var a = query["a"]; // => ["1", "3"]
```

Create URI from string:

```csharp
Uri uri = "http://host/path?a=1&b=2".ToUri();
```

Build a URI:

```csharp
Uri uri = Url.Create()
    .With(Url.Scheme.Http)
    .WithHostname("www.iana.org")
    .WithPath("about")
    .WithQueryParameter("a", "1")
    .WithFragment("chapter1");

Console.WriteLine(uri.AbsoluteUri); // => "http://www.iana.org/about?a=1#chapter1"
```

Start with an existing one and build a URI:

```csharp
Uri uri = Url.Create("http://www.iana.org")
    .With(Url.Scheme.Https)
    .WithQueryParameter("a", "1");

Console.WriteLine(uri.AbsoluteUri); // => "https://www.iana.org/?a=1"
```
