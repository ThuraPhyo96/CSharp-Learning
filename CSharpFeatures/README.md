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