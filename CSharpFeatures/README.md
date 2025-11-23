# C# in Depth, 4th Edition (Jon Skeet)

## Chapter 2 
- Generics: 
          - to write general-purpose code without knowing what that type is beforehand
          - mainly used for 1. Collections 2. Delegates, particularly in LINQ 3. Async code 4. Nullable value types
- Collections before generics:
          - Arrays: 
                    - fixed size
                    - type-safe
          - Object-based collections: 
                    - ArrayList: get a compile-time error if you try to add anything else
                    - Hashtable
         - Specialized collections:
                    - StringCollection: can only store strings

- Type constraints:
1. Reference Type Constraint
   - Constraint Type => where T : class
   - Restriction => The type argument must be a reference type (a class, interface, or delegate)
   - Purpose => Guarantees that the type is an object that can be passed by reference and, importantly, can be safely checked for null
2. Value Type Constraint
   - Constraint Type => where T : struct
   - Restriction => The type argument must be a value type (a struct or an enum)
   - Purpose => This constraint guarantees that the type argument T is a value type that does not inherently support the concept of null. Its primary function is to enable the C# runtime and language to support Nullable value types (Nullable<T>) by ensuring that the underlying type being wrapped cannot itself be nullable, preventing recursive nesting (like Nullable<Nullable<int>>). This design allows the Nullable<T> struct to effectively express the absence of information using an internal bool hasValue flag alongside the stored value T
3. Constructor Constraint
   - Constraint Type => where T : new()
   - Restriction => The type argument must have a public parameterless constructor
   - Purpose => This constraint allows the generic class or method to create instances of the type argument T using the new operator without any parameters. It ensures that T can be instantiated without requiring any specific arguments, which is particularly useful when you need to create new objects of the generic type within the generic class or method
4. Conversion Constraint
   - Constraint Type => where T : SomeType
   - Restriction => The type argument (T) must be convertible to the specified SomeType. This means T must derive from, implement, or be convertible to the specified type
   - SomeType Options => SomeType can be a class (e.g., Control), an interface (e.g., IFormattable), or another type parameter (e.g., T1 : T2)
   - Purpose => This constraint restricts which types can be provided as type arguments to a generic type or method
   - Benefit => It allows the compiler to know about specific methods and members defined on SomeType, guaranteeing that the type argument supports certain functionality beyond those defined in System.Object. For example, using where T : IFormattable ensures that specialized formatting methods, such as ToString(string, IFormatProvider), can be called successfully within the generic code
   - Context => This constraint is necessary because C# generic variance rules often prevent conversions between concrete generic types even if their type arguments are convertible (e.g., a List<decimal> is not convertible to List<IFormattable>). By making the method generic and using the conversion constraint, this type-safety requirement is met

## Chapter 3 
- LINQ: is a set of technologies that allows developers to write integrated queries directly within the C# language syntax. It fundamentally changes how developers interact with data.
1. Query Expressions: A declarative, SQL-like syntax in C# that is translated by the compiler into method calls
2. Lambda Expressions: Used to simplify delegate construction and, critically, to be converted into Expression Trees when querying out-of-process data
3. Extension methods:  
  - Definition => An Extension Method is a specialized form of a static method that can be invoked as if it were an instance method on an object. This feature was introduced in C# 3.
  - Declaration => An extension method must be defined within a non-nested, nongeneric static class. The method signature includes the keyword this immediately before the first parameter. This first parameter specifies the target or extended type.
  - Purpose => The primary purposes of extension methods are to increase code readability and provide flexible extensibility
        1. Fluent Interfaces and Readability: They enable method chaining or a fluent syntax, significantly improving code readability. This allows developers to use the result of one method call as the starting point (this parameter) of the next call.
        2. Extending Existing Types: They allow new functionality to be added to classes or interfaces that cannot be modified directly, such as framework types provided by Microsoft or third parties. This allows you to add methods to custom interfaces or built-in interfaces (like IEnumerable<T>).
        3. LINQ Foundation: They are essential for the Language Integrated Query (LINQ) framework, allowing types like IEnumerable<T> (which were not originally designed for query operations) to gain querying abilities such as filtering and ordering.
        4. Efficiency and Backwards Compatibility (C# 7.2): C# 7.2 introduced support for ref or in parameters on the first argument, allowing developers to extend large struct types while avoiding expensive copying, thereby improving performance. Historically, they also helped developers tied to older .NET versions gain access to new features like async/await via NuGet packages.
4. Anonymous Types and Implicit Typing (var): Used to concisely represent the data shape (projection) returned by a query within a local scope
- Purpose => The core purposes of LINQ are centered on integrating data access directly into the C# language:
        1. Unified Data Access: It provides simple data access and a unified syntax for querying both in-memory collections (LINQ to Objects) and out-of-process data (like SQL databases).
        2. Increased Readability: It enables developers to express **complex queries simply**.
        3. Efficient Remote Querying: For out-of-process data, LINQ uses Expression Trees to represent the query code as data, allowing a provider to analyze and convert the logic into an efficient query language, such as SQL.
        4. Functional Thinking: It encouraged C# developers to adopt a new approach to data transformations based on principles of functional programming
- Pros (Advantages): LINQ’s integrated approach offers several benefits:
        1. Statically Typed Safety: It provides static typing benefits such as compile-time checking and IntelliSense.
        2. Readability: It enables a fluent syntax and method chaining via extension methods. When successfully implemented, the features fit together beautifully.
        3. Conciseness: It addresses the verbosity downside of some statically typed languages by allowing local data shapes (projections) to be created concisely via anonymous types and implicit typing.
        4. Efficiency: For querying external data, expression trees ensure the query logic is passed to the provider to execute efficiently at the database level
- Cons (Disadvantages and Limitations): The implementation and usage of LINQ present specific challenges:
        1. Syntax Preference: While query expressions are excellent for complex operations involving joins or groupings, they add "baggage" for simpler queries (like a single filter operation). In these cases, method syntax (.Where(…)) is often easier to write. Developers are recommended to be comfortable with both styles.
        2. Expression Tree Constraints: Only expression-bodied lambda expressions can be converted into expression trees. Furthermore, expression trees cannot use the assignment operator, C# 4's dynamic typing, or C# 5's asynchrony.
        3. Anonymous Type Scope: Anonymous types, though concise, are limited in scope. You cannot return them easily from methods or properties without using object or dynamic, which sacrifices the benefits of static type safety.
        4. Query Re-evaluation: If an IQueryable<T> is enumerated repeatedly, the query will be evaluated again, potentially leading to inefficient database access