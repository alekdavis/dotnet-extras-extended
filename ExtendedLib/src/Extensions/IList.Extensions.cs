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
        int removed = 0;
 
        // Iterate through the elements in reverse to safely remove items while iterating
        for (int i = elements.Count - 1; i >= 0; i--)
        {
            object? element = elements[i];

            if (element == null)
            {
                continue;
            }

            if (elementToMatch.IsPartiallyEquivalentTo(elements[i], includeNonPublic))
            {
                elements.RemoveAt(i);
                removed++;
            }
        }
 
        return removed;
    }
}
