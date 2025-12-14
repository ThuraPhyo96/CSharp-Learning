# C# Extension Methods Notes

## What are extension methods in C#?
Extension methods add new methods to existing types without modifying their source or creating derived types. They are static methods in static classes, with the first parameter preceded by `this` to indicate the extended type.

Common uses:
```
1. Add convenience APIs to BCL types (e.g., string, DateTime, IEnumerable)
2. Provide fluent helpers for domain types
3. Keep utility logic discoverable via instance-like syntax
```
---

## 1. Declaring an Extension Method
```
public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? value)
        => string.IsNullOrEmpty(value);
}

// Usage
string? s = null;
bool result = s.IsNullOrEmpty(); // true
```
Key takeaway:
- Must be defined in a static class.
- Must be a static method with `this` on the first parameter.
---

## 2. Extension Methods on Custom Types
```
public class User { public string? Name { get; set; } }

public static class UserExtensions
{
    public static bool HasValidName(this User user)
        => !string.IsNullOrWhiteSpace(user?.Name);
}

// Usage
var u = new User { Name = "Alice" };
bool ok = u.HasValidName();
```
Key takeaway:
- You can extend your own types to keep validation/utilities close to usage.
---

## 3. Instance Member Precedence
If a type defines an instance method with the same signature, it takes precedence over the extension method.
```
public class User
{
    public string? Name { get; set; }
    // Uncommenting this will hide the extension method with the same signature
    // public bool HasValidName() => !string.IsNullOrWhiteSpace(Name);
}
```
Key takeaway:
- Extension methods are only considered when an instance method isn’t available.
---

## 4. Useful LINQ-Style Extensions for IEnumerable<T>
```
public static class EnumerableExtensions
{
    public static decimal TotalPrice(this IEnumerable<Product> products)
        => products.Sum(p => p.Price);
}
```
Key takeaway:
- Extensions compose well with LINQ to add domain-specific operations.
---

## 5. DateTime and Utility Extensions
```
public static class DateTimeExtensions
{
    public static bool IsWeekend(this DateTime date)
        => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
}
```
Key takeaway:
- Add readable helpers without changing the original type.
---

## Rules and Best Practices
```
- Keep extension methods in clearly named static classes
- Avoid polluting namespaces with overly generic names
- Prefer specific, discoverable helpers over catch-all utility methods
- Be careful with nullability; `this` parameter can be a nullable reference type
```
---

## Included Exercises
```
CSharpFeatures/ExtensionMethods/ contains examples:
- ExtendBasic.cs
- ExtendCustom.cs
- EnumerableExtensions.cs
- DateTimeExtensions.cs
```

### Key takeaway
- Extension methods enable instance-like calls on types you don’t own.
- Instance methods win over extension methods when signatures match.
- Organize extensions by domain and keep them in separate namespaces to avoid collisions.
