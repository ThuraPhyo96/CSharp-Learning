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