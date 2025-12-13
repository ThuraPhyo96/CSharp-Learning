# C# Interface Notes

## What is an `interface` in C#?
An interface defines a contract (a set of members) that implementing types must provide. It contains no implementation by default.

Interfaces are commonly used to:
```
1. Enable polymorphism without inheritance
2. Decouple components (program to abstractions)
3. Support dependency injection (DI)
4. Define capabilities (e.g., IDisposable, IEnumerable)
```
---

## 1. Declaring and Implementing Interfaces
```
public interface IWorker
{
    string Name { get; }
    void DoWork();
}

public class Engineer : IWorker
{
    public string Name { get; }
    public Engineer(string name) => Name = name;
    public void DoWork() => Console.WriteLine($"{Name} is building systems.");
}
```
Key takeaway:
- Classes implement interfaces using the syntax `class MyClass : IMyInterface`.
- All interface members must be implemented.
---

## 2. Multiple Interfaces
A class can implement multiple interfaces.
```
public interface ILogging
{
    void Log(string message);
}

public interface IAuditing
{
    void Audit(string action);
}

public class Service : ILogging, IAuditing
{
    public void Log(string message) => Console.WriteLine(message);
    public void Audit(string action) => Console.WriteLine($"Audit: {action}");
}
```
Key takeaway:
- Interfaces provide flexible composition of behaviors.
---

## 3. Polymorphism with Interfaces
Work with abstractions to enable flexible substitution.
```
public interface IShape
{
    double Area();
}

public class Rectangle : IShape
{
    public double Width { get; }
    public double Height { get; }
    public Rectangle(double w, double h) { Width = w; Height = h; }
    public double Area() => Width * Height;
}

public class Circle : IShape
{
    public double Radius { get; }
    public Circle(double r) { Radius = r; }
    public double Area() => Math.PI * Radius * Radius;
}

public static double TotalArea(IEnumerable<IShape> shapes) => shapes.Sum(s => s.Area());
```
Key takeaway:
- Code against `IShape` rather than concrete classes to support extension.
---

## 4. Interfaces and Dependency Injection (DI)
Interfaces are central to DI: inject abstractions into consumers.
```
public interface IRepository
{
    Task SaveAsync(string item);
}

public class FileRepository : IRepository
{
    public Task SaveAsync(string item)
    {
        // ... write to file ...
        return Task.CompletedTask;
    }
}

public class Processor
{
    private readonly IRepository _repo;
    public Processor(IRepository repo) { _repo = repo; }
    public Task ProcessAsync(string item) => _repo.SaveAsync(item);
}
```
Key takeaway:
- Swap implementations without changing consumers.
---

## Advanced Interface Topics (C# 8+)
```
Default interface members: interfaces can provide default implementations.
Explicit interface implementation: restricts member visibility through interface type.
Generic interfaces: increase type safety and reuse.
```
Example — default interface member:
```
public interface IGreeter
{
    void Greet(string name);
    void Hello(string name) => Console.WriteLine($"Hello, {name}");
}
```
---

## Included Exercises
```
CSharpFeatures/Interface/ contains examples:
- InterfaceImplementation.cs
- InterfacePolymorphism.cs
- MultipleInterfaces.cs
- InterfaceWithDI.cs
```

### Key takeaway
- Interfaces define contracts and encourage decoupled design.
- A class can implement multiple interfaces.
- Interfaces enable polymorphism and DI.
- Modern C# supports default interface methods for shared behavior.