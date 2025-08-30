# DeepCopyTests

This project tests several popular libraries implementing the deep copy functionality for complex data types, to see which one of them is the most robust.

## Libraries

The following libraries were tested:

- [DeepCloner](https://www.nuget.org/packages/DeepCloner)
  - Version: 0.10.4
  - Result: PASSED
- [DeepCopy](https://www.nuget.org/packages/DeepCopy)
  - Version: 1.0.3
  - Result: FAILED (caused an exception)
  - Comment: Caused an exception: *System.InvalidProgramException : Common Language Runtime detected an invalid program.*
- [DeepCopy.Expression](https://www.nuget.org/packages/DeepCopy.Expression)
  - Version: 1.5.0
  - Result: FAILED
  - Comment: Caused an exception: *System.ArgumentException : Type is not supported.*
- [FastDeepCloner](https://www.nuget.org/packages/FastDeepCloner)
  - Version: 1.3.6
  - Result: FAILED (almost passed)
  - Comment: Did not cause an exception, but a sensitive property holding a password value, which may be a good thing depending on the requirement.

## Details

While most libraries can deep copy simple data structures, most of them struggle with complex data types. For testing, we use the [Microsoft.Graph.Models.User](https://learn.microsoft.com/en-us/graph/api/resources/user) class, which has a complex structure with nested objects, collections, and various data types, including sensitive properties, such as `Password`. To see the results, run the unit tests defined in this project.

## Conclusion

At this time, the only library that successfully deep copied the complex data structure without causing exceptions or losing data is [DeepCloner](https://www.nuget.org/packages/DeepCloner). It is the one that we use internally for the cloning functionality, but if things change and some other library proves to be more robust, we may consider switching to it in the future.