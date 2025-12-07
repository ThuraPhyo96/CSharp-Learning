# C# Sealed Keyword Notes

## What is `sealed` in C#?
The `sealed` keyword prevents:
```
1. A class from being inherited
2. A method (that was overridden) from being overridden again
```
		

It is commonly used to:
```
1. Protect class behavior
2. Improve runtime performance (JIT optimizations)
3. Enforce design rules in shared libraries
4. Avoid dangerous or incorrect inheritance
```
---

## 1. Sealed Classes
A sealed class cannot be inherited.
```
sealed class BankAccount
{
    public void PrintBalance()
    {
        Console.WriteLine("Balance: $1000");
    }
}

// Error — cannot inherit sealed class
// class SavingsAccount : BankAccount { }
```
---

## 2. Sealed Methods
Can use `sealed` inside an override to prevent further overriding.
```
class Vehicle
{
    public virtual void Start() { }
}

class Car : Vehicle
{
    public sealed override void Start()
    {
        Console.WriteLine("Car starting...");
    }
}

// Not allowed
// class SportsCar : Car
// {
//     public override void Start() { }
// }
```
A method can only be sealed when it is also an override.

---

## Why Use sealed at All?
```
To protect behavior
Want to prevent developers from modifying sensitive logic.

To improve performance
The JIT compiler optimizes sealed classes and sealed methods because their inheritance chain is fixed.

To avoid inheritance misuse
Sometimes inheritance causes bugs or violates design principles (like LSP).

To create utility classes
Sealed classes work well for static helpers.
```

---

## Included Exercises
```
CSharpFeatures/Sealed/ contains 5 exercises that demonstrate different uses of the sealed keyword.
```

---

## Sealed Class
```
File: BasicSealedClassUsage.cs
Shows how a sealed class cannot be inherited.

Key takeaway:
	Sealed classes stop inheritance at the class level.
```

---

## Sealed Method
```
File: BasicSealedMethodUsage.cs
Demonstrates how to seal an overridden method to prevent further overrides.

Key takeaway:
	A sealed method must be an override, and stops overriding in subclasses.
```

---

## Sealed in Polymorphism
```
File: CombinedSealedWithPolymorphism.cs
Shows sealed method behavior in real-world polymorphic usage.

Key takeaway:
	Even in polymorphism, sealed methods ensure fixed behavior in deeper inheritance chains.
```

---

## Sealed Utility Class
```
File: SealedUtilityClass.cs
Demonstrates using sealed classes for utility/static helper methods.

Key takeaway:
	Sealed classes are useful when inheritance is not meaningful.
```

---

## Sealed for Business Logic Protection
```
File: SealedForProtection.cs
Shows how sealed can protect sensitive or critical logic (e.g., payment processing).

Key takeaway:
	Sealed methods ensure logic cannot be changed by subclasses.
```

---