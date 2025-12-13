# C# Abstract Keyword Notes

## What is `abstract` in C#?
The `abstract` keyword is used to define incomplete members or types that must be completed by derived classes.

It can be applied to:
```
1. Classes — cannot be instantiated and may contain abstract members
2. Methods — declared without implementation; must be overridden
3. Properties/Indexers/Events — declared without implementation; must be overridden
```
---

## 1. Abstract Classes
An abstract class cannot be instantiated. It can contain both implemented and abstract members.
```
public abstract class Shape
{
    // Implemented member
    public string Name { get; }

    protected Shape(string name) => Name = name;

    // Abstract member — must be implemented in derived types
    public abstract double Area { get; }
}

// Error — cannot instantiate abstract class
// var s = new Shape("Circle");
```
---

## 2. Abstract Methods and Properties
Abstract members declare a contract that derived classes must fulfill.
```
public abstract class Animal
{
    public abstract string Species { get; }
    public abstract void Speak();
}

public class Dog : Animal
{
    public override string Species => "Dog";
    public override void Speak() => Console.WriteLine("Bark!");
}
```
---

## 3. Mixed Members Pattern
Abstract classes often provide shared implementation plus abstract extension points.
```
public abstract class Human
{
    protected string _name;
    protected Human(string name) => _name = name;

    // Implemented shared behavior
    public void Walk() => Console.WriteLine($"{_name} is walking.");

    // Abstract contract to be provided by subclasses
    public abstract string JobTitle { get; }
    public abstract void Job();
}

public class Doctor : Human
{
    public Doctor(string name) : base(name) {}
    public override string JobTitle => "Doctor";
    public override void Job() => Console.WriteLine($"{_name} is treating patients.");
}
```
---

## Why Use abstract?
```
To define a common base with shared logic
Encapsulate reusable behavior while forcing specific implementations.

To enforce a contract
Guarantee derived classes provide required members.

To support polymorphism
Work with base types while executing derived behavior.
```

---

## Included Exercises
```
CSharpFeatures/Abstract/ contains examples showing abstract classes with mixed implemented and abstract members.
File: AbstractWithMixedMembers.cs
```

### Key takeaway
- Abstract classes cannot be instantiated.
- Derived classes must implement all abstract members.
- Abstract classes can include implemented members to share behavior.

---

## Abstract vs. sealed
```
abstract: encourages inheritance and requires overrides.
sealed: prevents inheritance and further overrides.
```

Use `abstract` to define extension points in your design. Use `sealed` to lock behavior and prevent extension.
