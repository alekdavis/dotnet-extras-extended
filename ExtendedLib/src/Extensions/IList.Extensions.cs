using DotNetExtras.Common.Extensions;
using System.Collections;

namespace DotNetExtras.Extended;
/// <summary>
/// Implements advanced extension methods for the <see cref="IList"/> types.
/// </summary>
public static partial class IListExtensions
{
    /// <summary>
    /// Removes all items in the list that match the values of 
    /// the specified properties in the provided item.
    /// </summary>
    /// <param name="elements">
    /// List items.
    /// </param>
    /// <param name="elementToMatch">
    /// Item holding property values that will need to match for the list elements to be removed.
    /// </param>
    /// <param name="includeNonPublic">
    /// If <c>true</c>, non-public properties and fields will be checked along with the public properties and fields.
    /// </param>
    /// <returns>
    /// Number of removed elements.
    /// </returns>
    /// <example>
    /// <code>
    /// <![CDATA[
    /// List<Sample> elements = new()
    /// {
    ///     new(){ Id = 100, ParentId = 1, Name = "Item1" },
    ///     new(){ Id = 200, ParentId = 2, Name = "Item2" },
    ///     new(){ Id = 300, ParentId = 2, Name = "Item3" },
    ///     new(){ Id = 400, ParentId = 3, Name = "Item4" }
    /// };
    /// 
    /// Sample match = new() { ParentId = 2 };
    ///
    /// // Removes two items:
    /// int removedCount = elements.RemoveMatching(match);
    /// ]]>
    /// </code>
    /// </example>
    public static int RemoveMatching
    (
        this IList elements, 
        object elementToMatch,
        bool includeNonPublic = false
    )
    {
        // Removing all matches is equivalent to removing with an unbounded limit.
        return elements.RemoveMatching(elementToMatch, int.MaxValue, includeNonPublic);
    }

    /// <summary>
    /// Removes up to the specified number of items in the list that match the values of
    /// the specified properties in the provided item, starting from the beginning of the list.
    /// </summary>
    /// <param name="elements">
    /// List items.
    /// </param>
    /// <param name="elementToMatch">
    /// Item holding property values that will need to match for the list elements to be removed.
    /// </param>
    /// <param name="limit">
    /// Maximum number of matching elements to remove (must be a positive number).
    /// With <c>limit</c> set to 1, only the first match (from the beginning) will be removed;
    /// with <c>limit</c> set to 2, only the first two matches will be removed, etc.
    /// </param>
    /// <param name="includeNonPublic">
    /// If <c>true</c>, non-public properties and fields will be checked along with the public properties and fields.
    /// </param>
    /// <returns>
    /// Number of removed elements.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when <paramref name="limit"/> is less than 1.
    /// </exception>
    /// <example>
    /// <code>
    /// <![CDATA[
    /// List<Sample> elements = new()
    /// {
    ///     new(){ Id = 100, ParentId = 1, Name = "Item1" },
    ///     new(){ Id = 200, ParentId = 2, Name = "Item2" },
    ///     new(){ Id = 300, ParentId = 2, Name = "Item3" },
    ///     new(){ Id = 400, ParentId = 3, Name = "Item4" }
    /// };
    /// 
    /// Sample match = new() { ParentId = 2 };
    ///
    /// // Removes only the first matching item (Item2):
    /// int removedCount = elements.RemoveMatching(match, 1);
    /// ]]>
    /// </code>
    /// </example>
    public static int RemoveMatching
    (
        this IList elements,
        object elementToMatch,
        int limit,
        bool includeNonPublic = false
    )
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(limit, 1);

        int removed = 0;

        // Iterate from the beginning to remove the earliest matches first.
        for (int i = 0; i < elements.Count; )
        {
            object? element = elements[i];

            if (element == null)
            {
                i++;
                continue;
            }

            if (elementToMatch.IsPartialEquivalentOf(elements[i], includeNonPublic))
            {
                elements.RemoveAt(i);
                removed++;

                if (removed >= limit)
                {
                    break;
                }
            }
            else
            {
                i++;
            }
        }

        return removed;
    }
}
