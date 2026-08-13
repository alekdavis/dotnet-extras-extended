using ExtendedLibTests.Models;
using DotNetExtras.Extended;

namespace ExtendedLibTests;
public partial class ExtensionsTests
{
    [Fact]
    public void IList_RemoveMatching()
    {
        int count, total = 0, originalCount;

        List<User> users =
        [
            new() { Name = new() { GivenName = "Alice", Surname = "Wonder" }, Age = 10, Sponsor = new() { Id = "12345" }, },
            new() { Name = new() { GivenName = "Jack", Surname = "Black" }, Age = 13, Sponsor = new() { Id = "54321" }, },
            new() { Name = new() { GivenName = "Magic", Surname = "Seven" }, Age = 25, Sponsor = new() { Id = "98765" }, },
            new() { Name = new() { GivenName = "Mary", Surname = "Jones" }, Age = 20, Sponsor = new() { Id = "12345" }, },
            new() { Name = new() { GivenName = "Swift", Surname = "Bracket" }, Age = 20, Sponsor = new() { Id = "98765" }, },
            new() { Name = new() { GivenName = "Flint", Surname = "Eastwood" }, Age = 25, Sponsor = new() { Id = "13579" }, },
            new() { Name = new() { GivenName = "Magic", Surname = "Eight" }, Age = 25, Sponsor = new() { Id = "98765" }, },
        ];

        originalCount = users.Count;

        count = users.RemoveMatching(new User() { Name = new() { GivenName = "Alice" }}, true);
        total += count;

        Assert.Equal(1, count);
        Assert.Equal(originalCount -  total, users.Count);
        Assert.DoesNotContain(users, u => u.Name?.GivenName == "Alice");

        count = users.RemoveMatching(new User() { Age = 25, Sponsor = new() { Id = "98765" } }, true);
        total += count;

        Assert.Equal(2, count);
        Assert.Equal(originalCount -  total, users.Count);
        Assert.DoesNotContain(users, u => u.Age == 25 && u.Sponsor?.Id == "98765");
    }

    [Fact]
    public void IList_RemoveMatching_Limit()
    {
        List<User> users =
        [
            new() { Name = new() { GivenName = "Alice", Surname = "Wonder" }, Age = 10, Sponsor = new() { Id = "12345" }, },
            new() { Name = new() { GivenName = "Jack", Surname = "Black" }, Age = 25, Sponsor = new() { Id = "98765" }, },
            new() { Name = new() { GivenName = "Magic", Surname = "Seven" }, Age = 25, Sponsor = new() { Id = "98765" }, },
            new() { Name = new() { GivenName = "Mary", Surname = "Jones" }, Age = 25, Sponsor = new() { Id = "98765" }, },
            new() { Name = new() { GivenName = "Swift", Surname = "Bracket" }, Age = 20, Sponsor = new() { Id = "13579" }, },
        ];

        // Removing with limit=1 removes only the first match (Jack).
        int count = users.RemoveMatching(new User() { Age = 25, Sponsor = new() { Id = "98765" } }, 1, true);

        Assert.Equal(1, count);
        Assert.Equal(4, users.Count);
        Assert.DoesNotContain(users, u => u.Name?.GivenName == "Jack");
        Assert.Contains(users, u => u.Name?.GivenName == "Magic");
        Assert.Contains(users, u => u.Name?.GivenName == "Mary");

        // Removing with limit=2 removes the next two matches (Magic and Mary).
        count = users.RemoveMatching(new User() { Age = 25, Sponsor = new() { Id = "98765" } }, 2, true);

        Assert.Equal(2, count);
        Assert.Equal(2, users.Count);
        Assert.DoesNotContain(users, u => u.Age == 25 && u.Sponsor?.Id == "98765");

        // A limit larger than the number of matches removes only the available matches.
        count = users.RemoveMatching(new User() { Age = 10 }, 100, true);

        Assert.Equal(1, count);
        Assert.Single(users);

        // A non-positive limit is not allowed.
        Assert.Throws<ArgumentOutOfRangeException>(
            () => users.RemoveMatching(new User() { Age = 20 }, 0, true));
    }

    [Fact]
    public void IList_RemoveMatching_NoMatch()
    {
        List<User> users =
        [
            new() { Name = new() { GivenName = "Alice" }, Age = 10 },
            new() { Name = new() { GivenName = "Jack" }, Age = 25 },
        ];

        // Remove-all overload: nothing matches, list is unchanged.
        int count = users.RemoveMatching(new User() { Age = 99 }, true);

        Assert.Equal(0, count);
        Assert.Equal(2, users.Count);

        // Limit overload: nothing matches, list is unchanged.
        count = users.RemoveMatching(new User() { Age = 99 }, 1, true);

        Assert.Equal(0, count);
        Assert.Equal(2, users.Count);
    }

    [Fact]
    public void IList_RemoveMatching_EmptyList()
    {
        List<User> users = [];

        Assert.Equal(0, users.RemoveMatching(new User() { Age = 10 }, true));
        Assert.Equal(0, users.RemoveMatching(new User() { Age = 10 }, 5, true));
        Assert.Empty(users);
    }

    [Fact]
    public void IList_RemoveMatching_NullElements()
    {
        List<User?> users =
        [
            null,
            new() { Name = new() { GivenName = "Alice" }, Age = 25 },
            null,
            new() { Name = new() { GivenName = "Jack" }, Age = 25 },
            null,
        ];

        // Null entries must be skipped safely while matches are removed.
        int count = users.RemoveMatching(new User() { Age = 25 }, true);

        Assert.Equal(2, count);
        Assert.Equal(3, users.Count);
        Assert.All(users, u => Assert.Null(u));

        // Same behavior for the limit overload (limit=1 removes the first match only).
        users =
        [
            null,
            new() { Name = new() { GivenName = "Alice" }, Age = 25 },
            null,
            new() { Name = new() { GivenName = "Jack" }, Age = 25 },
        ];

        count = users.RemoveMatching(new User() { Age = 25 }, 1, true);

        Assert.Equal(1, count);
        Assert.Equal(3, users.Count);
        Assert.DoesNotContain(users, u => u?.Name?.GivenName == "Alice");
        Assert.Contains(users, u => u?.Name?.GivenName == "Jack");
    }

    [Fact]
    public void IList_RemoveMatching_NegativeLimit()
    {
        List<User> users =
        [
            new() { Name = new() { GivenName = "Alice" }, Age = 10 },
        ];

        Assert.Throws<ArgumentOutOfRangeException>(
            () => users.RemoveMatching(new User() { Age = 10 }, -5, true));

        // The list must remain untouched when validation fails.
        Assert.Single(users);
    }
}
