using Force.DeepCloner;
using System.Dynamic;
using System.Reflection;

namespace DotNetExtras.Extended;
/// <summary>
/// Implements advanced extension methods for the <see cref="object"/> types.
/// </summary>
public static partial class ObjectExtensions
{
    /// <summary>
    /// Converts any object to a dynamic object.
    /// </summary>
    /// <remarks>
    /// Adapted from
    /// <see href="https://stackoverflow.com/questions/42836936/convert-class-to-dynamic-and-add-propertyNames#answer-42837044"/>.
    /// For dictionary objects, 
    /// use the <see cref="DictionaryExtensions.ToDynamic(Dictionary{string, object?})"/> method instead.
    /// </remarks>
    /// <typeparam name="T">
    /// Data type of the source object.
    /// </typeparam>
    /// <param name="source">
    /// The original object.
    /// </param>
    /// <param name="extras">
    /// Additional propertyNames to be added to the expando object.
    /// </param>
    /// <param name="publicOnly">
    /// If true, only public properties will be included.
    /// </param>
    /// <returns>
    /// Expando object.
    /// </returns>
    /// <example>
    /// <code>
    /// User user = new()
    /// {
    ///     Name = new()
    ///     {
    ///         GivenName = "John",
    ///         Surname = "Doe"
    ///     },
    ///     Age = 42,
    ///     Mail = "John.Doe@mail.com",
    /// };
    ///
    /// Dictionary&lt;string, object&gt; extras = new()
    /// {
    ///     { "ExtraProperty", "XYZ" }
    /// };
    ///
    /// dynamic? result = user.ToDynamic(extras);
    /// </code>
    /// </example>
    public static dynamic? ToDynamic<T>
    (
        this T source,
        Dictionary<string, object>? extras = null,
        bool publicOnly = false
    )
    {
        IDictionary<string, object?> expando = new ExpandoObject();

        foreach (PropertyInfo propertyInfo in typeof(T).GetProperties(
            BindingFlags.Instance | 
            BindingFlags.Public | 
            (publicOnly ? 0 : BindingFlags.NonPublic)))
        {
            object? currentValue = propertyInfo.GetValue(source);

            if (currentValue != null)
            {
                expando.Add(propertyInfo.Name, currentValue);
            }
        }

        if (extras != null)
        {
            foreach (string key in extras.Keys)
            {
                if (extras[key] != null)
                {
                    expando.Add(key, extras[key]);
                }
            }
        }

        return expando as ExpandoObject;
    }

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
            : original.DeepClone<T>();
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
        return original?.DeepClone();
    }
}
