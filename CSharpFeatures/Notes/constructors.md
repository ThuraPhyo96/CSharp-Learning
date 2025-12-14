# C# Constructors Notes

## What is a constructor in C#?
A constructor initializes a new instance of a type. It runs when an object is created and can set up state or perform one-time actions.

Types of constructors:
```
1. Instance constructors (parameterless and parameterized)
2. Static constructors (run once per type)
3. Copy-like patterns (not a language feature, but common)
```
---

## 1. Instance Constructors
Create and initialize object state.
```
public class User
{
    public string Name { get; }
    public int Age { get; }

    // Parameterless (default) constructor
    public User()
    {
        Name = "Unknown";
        Age = 0;
    }

    // Overloaded (parameterized) constructor
    public User(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
```
Key takeaway:
- You can overload constructors to support multiple initialization paths.
- Use `this(...)` to chain constructors and avoid duplication.
---

## 2. Constructor Overloading and Chaining
```
public class Product
{
    public string Name { get; }
    public decimal Price { get; }

    public Product() : this("Unknown", 0m) { }
    public Product(string name) : this(name, 0m) { }
    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}
```
Key takeaway:
- Constructor chaining centralizes initialization.
---

## 3. Static Constructors
A static constructor initializes static data for a type.
```
public class Cache
{
    private static readonly Dictionary<string, string> _store;

    static Cache()
    {
        _store = new Dictionary<string, string>();
        Console.WriteLine("Cache static constructor ran once.");
    }

    public static void Add(string key, string value) => _store[key] = value;
}
```
Rules:
```
- No access modifiers or parameters
- Runs once per type before first use
- Called automatically by the runtime
```
---

## 4. Parameter Rules for Static Constructors
Static constructors cannot have parameters.
```
public class Config
{
    static Config() { /* OK */ }
    // static Config(string path) { } // Not allowed
}
```
---

## 5. Using Constructors in .NET 8 / C# 12
Modern C# still follows the same rules for constructors, with improvements elsewhere (like primary constructors for records/structs in newer versions). For classes, standard constructors are used.
---

## Included Exercises
```
CSharpFeatures/Constructors/ contains examples:
- StaticConstructorRunsOnce.cs
- StaticConstructorRunsBeforeFirstUse.cs
- ParametersNotAllowedInStaticConstructor.cs
- InstanceConstructorOverloading.cs
```

### Key takeaway
- Instance constructors initialize object state; overload for flexibility.
- Static constructors run once and cannot have parameters or access modifiers.
- Use constructor chaining to avoid duplication.
