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