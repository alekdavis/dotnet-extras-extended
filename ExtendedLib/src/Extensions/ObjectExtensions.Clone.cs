namespace DotNetExtras.Extended;
/// <summary>
/// Implements advanced extension methods for the <see cref="object"/> types.
/// </summary>
public static partial class ObjectExtensions
{
    /// <summary>
    /// Returns a deep copy of a strongly typed object (does not require type casting).
    /// </summary>
    /// <typeparam name="T">
    /// Object type.
    /// </typeparam>
    /// <param name="original">
    /// Original object.
    /// </param>
    /// <returns>
    /// Cloned object.
    /// </returns>
    /// <remarks>
    /// Uses 
    /// <see href="https://github.com/force-net/DeepCloner"/>
    /// (the only library that can handle copying all properties of the <c>Microsoft.Graph.Models.User</c>
    /// objects).
    /// </remarks>
    /// <example>
    /// <code>
    /// MyObject? cloneObject = originalObject.Clone();
    /// </code>
    /// </example>
    public static T? Clone<T>
    (
        this T original
    )
    {
        return original == null 
            ? default 
            : FastCloner.FastCloner.DeepClone<T>(original);
    }

    /// <inheritdoc cref="Clone{T}(T)" path="param|returns|remarks"/>
    /// <summary>
    /// Returns a deep copy of an object (requires type casting).
    /// </summary>
    /// <example>
    /// <code>
    /// MyObject? cloneObject = (MyObject?)originalObject.Clone();
    /// </code>
    /// </example>
    public static object? Clone
    (
        this object? original
    )
    {
        return original == null
            ? null
            : FastCloner.FastCloner.DeepClone(original);
    }
}
