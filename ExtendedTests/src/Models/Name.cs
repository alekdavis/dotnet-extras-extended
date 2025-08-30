using System.Text.Json.Serialization;

namespace ExtendedLibTests.Models;
internal class Name
{
    [JsonInclude]
    internal string? Surname { get; set; }

    [JsonInclude]
    internal string? GivenName { get; set; }

    [JsonInclude]
    internal string? MiddleName { get; set; }
}
