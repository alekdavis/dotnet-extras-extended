using System.Text.Json.Serialization;

namespace ExtendedLibTests.Models;

internal enum PhoneType: int
{
    [JsonInclude]
    Personal = 0,
    [JsonInclude]
    Business = 1,
    [JsonInclude]
    Other = 2
}
