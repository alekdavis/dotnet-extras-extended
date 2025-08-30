using ExtendedLibTests.Models;
using DotNetExtras.Extended;

namespace ExtendedLibTests;

public partial class ExtensionsTests
{
    [Fact]
    public void Object_ToDynamic()
    {
        User user = new()
        {
            Name = new()
            {
                GivenName = "John",
                Surname = "Doe"
            },
            Age = 42,
            Mail = "John.Doe@mail.com",
            OtherMail = ["DoeJohn@mail.com"],
            Phones =
            [
                new() { Number = "123-456-7890", Type = PhoneType.Personal },
                new() { Number = "987-654-3210", Type = PhoneType.Business },
            ],
        };

        Dictionary<string, object> extras = new()
        {
            { "ExtraProperty", "XYZ" }
        };

        dynamic? result = user.ToDynamic(extras);

        Assert.Equal("John", result?.Name.GivenName);
        Assert.Equal("Doe", result?.Name.Surname);
        Assert.Equal(42, result?.Age);
        Assert.Equal("John.Doe@mail.com", result?.Mail);
        Assert.Equal("DoeJohn@mail.com", result?.OtherMail[0]);
        Assert.Equal("123-456-7890", result?.Phones[0].Number);
        Assert.Equal(PhoneType.Personal, result?.Phones[0].Type);
        Assert.Equal("987-654-3210", result?.Phones[1].Number);
        Assert.Equal(PhoneType.Business, result?.Phones[1].Type);
        Assert.Equal("XYZ", result?.ExtraProperty);
    }

    [Fact]
    public void Object_Clone()
    {
        User user = new()
        {
            Name = new()
            {
                GivenName = "John",
                Surname = "Doe"
            },
            Age = 42,
            Mail = "John.Doe@mail.com",
            OtherMail = ["DoeJohn@mail.com"],
            Phones =
            [
                new() { Number = "123-456-7890", Type = PhoneType.Personal },
                new() { Number = "987-654-3210", Type = PhoneType.Business },
            ],
        };

        User? result = user.Clone();

        Assert.Equal("John", result?.Name?.GivenName);
        Assert.Equal("Doe", result?.Name?.Surname);
        Assert.Equal(42, result?.Age);
        Assert.Equal("John.Doe@mail.com", result?.Mail);
        Assert.Equal("DoeJohn@mail.com", result?.OtherMail?[0]);
        Assert.Equal("123-456-7890", result?.Phones?[0].Number);
        Assert.Equal(PhoneType.Personal, result?.Phones?[0].Type);
        Assert.Equal("987-654-3210", result?.Phones?[1].Number);
        Assert.Equal(PhoneType.Business, result?.Phones?[1].Type);
    }
}
