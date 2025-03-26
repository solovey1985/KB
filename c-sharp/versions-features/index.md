


- [C# Deep Dive: Newer Features \[TOC\]](#c-deep-dive-newer-features-toc)
  - [C# 8.0](#c-80)
  - [C# 9.0](#c-90)
  - [C# 10.0 and beyond (as of my last update)](#c-100-and-beyond-as-of-my-last-update)

## C# Deep Dive: Newer Features [TOC]
### C# 8.0

1. **Nullable Reference Types**: 
    - Introduced to help developers prevent `NullReferenceException`s.
    - By default, reference types are non-nullable. To declare them as nullable, use the `?` modifier.
    - Example: `string? nullableString;`
    
2. **Pattern Matching Enhancements**:
    - **Switch Expressions**: More concise syntax for switch statements.
      ```csharp
      var result = input switch
      {
          1 => "One",
          2 => "Two",
          _ => "Unknown"
      };
      ```
    - **Property Patterns**: Allows patterns to match properties within objects.
    - **Tuple Patterns**: Matches tuples against patterns.
    - **Positional Patterns**: Deconstructs an object and matches its properties.

3. **Asynchronous Streams**:
    - Introduction of `IAsyncEnumerable<T>` to represent async streams.
    - Use `await foreach` to asynchronously iterate over sequences.

4. **Indices and Ranges**:
    - Define ranges using `..` operator.
      ```csharp
      var subArray = array[1..4]; // retrieves items at index 1, 2, and 3.
      ```
    - Use `^` to indicate counting from the end.
      ```csharp
      var lastElement = array[^1];
      ```

5. **Default Interface Implementations**: Allows defining default implementations for methods in interfaces. This facilitates the evolution of interfaces without breaking existing implementations.

6. **Using Declarations**: Introduces a more concise syntax to dispose of objects.
   ```csharp
   using var stream = new FileStream("file.txt", FileMode.Open);
   ```

### C# 9.0

1. **Records**:
    - Provides value semantics for types.
    - Uses `with` expression for non-destructive mutation.
      ```csharp
      public record Person(string Name, int Age);
      var person = new Person("John", 30);
      var otherPerson = person with { Age = 31 };
      ```

2. **Init Only Setters**: Allows properties to be set during object initialization but prevents modification afterwards.
    ```csharp
    public class Example
    {
        public int Value { get; init; }
    }
    ```

3. **Pattern Matching Enhancements**:
    - **Relational Patterns**: Use relational operators in patterns (`<`, `>`, `<=`, `>=`).
    - **Logical Patterns**: Use logical operators to combine patterns (`and`, `or`, `not`).

4. **With Expressions**: Enables non-destructive mutation of records.

5. **Top-level Statements**: Removes the need for boilerplate code and allows main program logic to reside at the top level without being wrapped in a class or method.

6. **Target-typed `new` Expressions**: Removes the need to specify type when instantiating an object if the type can be inferred.
    ```csharp
    List<int> list = new();
    ```

7. **Covariant Returns**: Allows an overriding method to have a more derived return type than the method it overrides.

### C# 10.0 and beyond (as of my last update)

Since my last update was in September 2021, I won't have information on versions beyond C# 9.0. However, I would recommend checking the official Microsoft documentation or C# language GitHub repository for the latest features and updates.

It's important to understand not just the syntax of these new features but also their real-world applications, the problems they solve, and any potential pitfalls or things to be aware of when using them.