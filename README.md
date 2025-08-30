# DotNetExtras.Extended

`DotNetExtras.Extended` is a general-purpose .NET Core library that implements useful extension methods for common data types.

Use the `DotNetExtras.Extended` library to:

- Deep clone objects.
- Remove list elements that match the specified criteria.
- Check if a string value contains JSON.
- Check if a string value contains HTML.
- Convert a string to a different data type, such as <c>DateTime</c>.
- Convert a tokenized string to an array, dictionary, list, or hash set.

## Documentation

For complete documentation, usage details, and code samples, see:

- [Documentation](https://alekdavis.github.io/dotnet-extras-extended)
- [Unit tests](https://github.com/alekdavis/dotnet-extras-extended/tree/main/ExtendedTests)

## Usage

The following examples illustrates various operations implemented by the `DotNetExtras.Extended` library.

### Convert string values to simple data types

```cs
using DotNetExtras.Extended;
...
bool b = "true".ToType<bool>();
int n = "123".ToType<int>();
DateTime dt = "2021-10-11T17:54:38".ToType<DateTime>();
DateTimeOffset dto = "2021-10-11T17:54:38-03:30".ToType<DateTimeOffset>();
```
              
### Convert string values to arrays, lists, dictionaries, has sets

```cs
using DotNetExtras.Extended;
...
// Will hold: value1, value2, value3
string[] result = "value1|value2|value3".ToArray<string>(); 

// Will hold: value1, value2, value3
List<string> list = "value1|value2|value3".ToList<string>();

// Will hold: key1=value1, key2=value2
Dictionary<string,string> result = "key1=value1|key2=value2".ToDictionary<string, string>();

// Will hold: [1, 2, 3]
HashSet<int>? hashSet = "1|2|3".ToHashSet();

// Will hold: ["one", "two", "three"]
HashSet<string>? hashSet = "one,two,three".ToHashSet(",");
```
              
### Check if string contains a valid JSON or HTML document

```cs
using DotNetExtras.Extended;
...
bool isJson;

// This test can handle both a single element and an array.
isJson = "{\"key1\": \"value1\", \"key2\": \"value2\"}".IsJson(); // true
isJson = "[{\"key1\": \"value1\"}, {\"key2\": \"value2\", \"key3\": 123}]".IsJson(); // true

bool isHtml;

// This test only checks if the string starts with the html tag.
isHtml = "<!DOCTYPE html>hello</html>".IsHtml(); // true
isHtml = "<html>hello</html>".IsHtml(); // true
```

### Escape special LDAP characters in a string

```cs
using DotNetExtras.Extended;
...
// Will hold:
string escaped = "Hello, world!".EscapeLdapValue();
```

### Deep clone an object

```cs
using DotNetExtras.Extended;
...
User clone = original.Clone();
```

### Remove elements from the list

```cs
List<Sample> elements = new()
{
    new(){ Id = 100, ParentId = 1, Name = "Item1" },
    new(){ Id = 200, ParentId = 2, Name = "Item2" },
    new(){ Id = 300, ParentId = 2, Name = "Item3" },
    new(){ Id = 400, ParentId = 3, Name = "Item4" }
};

Sample match = new() { ParentId = 2 };

// Removes two items with ParentId = 2.
int removedCount = elements.RemoveMatching(match);
```

## Package

Install the latest version of the `DotNetExtras.Extended` NuGet package from:

- [https://www.nuget.org/packages/DotNetExtras.Extended](https://www.nuget.org/packages/DotNetExtras.Extended)

## See also

Check out other `DotNetExtras` libraries at:

- [https://github.com/alekdavis/dotnet-extras](https://github.com/alekdavis/dotnet-extras)
