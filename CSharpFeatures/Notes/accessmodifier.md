# C# Access Modifiers Notes

## What are access modifiers?
Access modifiers control the visibility and accessibility of types and members.
```
Included modifiers:
- public, 
- internal, 
- protected, 
- private,
- protected internal, 
- private protected, 
- file (file-scoped type)
```

---

## 1. public
Accessible from anywhere.
Source: CSharpFeatures/AccessModifiers/PublicClass.cs
```
public class PublicClass
{
    public string Message() => "Hello from PublicClass!";

    public static void Run()
    {
        PublicClass pc = new PublicClass();
        Console.WriteLine(pc.Message()); // accessing public method
    }
}
```
Key takeaway:
- Public members and types can be accessed from any assembly and any code location.
---

## 2. internal
Accessible only within the same assembly.
Source: CSharpFeatures/AccessModifiers/InternalClass.cs
```
internal class InternalClass
{
    internal void DisplayMessage()
    {
        Console.WriteLine("Hello from InternalClass!");
    }

    public static void Run()
    {
        InternalClass ic = new InternalClass();
        ic.DisplayMessage(); // accessing internal method
    }
}
```
Key takeaway:
- Internal types and members are visible only inside the project/assembly that defines them.
---

## 3. protected
Accessible within the class and its derived types.
Source: CSharpFeatures/AccessModifiers/ProtectedModifier.cs
```
class ProtectedModifier
{
    protected string name = "Protected Name";
}

class DerivedProtected : ProtectedModifier
{
    public static void Run()
    {
        var baseObject = new ProtectedModifier();
        var derivedObject = new DerivedProtected();

        // Error: cannot access protected through base type
        // baseObject.name = "New Name";

        // OK: accessible through derived type
        derivedObject.name = "Updated from derived class";
        Console.WriteLine($"After accessible: {derivedObject.name}");
    }
}
```
Key takeaway:
- Protected members are only accessible in derived types (not through base instances).
---

## 4. protected internal
Accessible from same assembly OR any derived type (even in other assemblies).
Source: CSharpFeatures/AccessModifiers/ProtectedInternalModifier.cs

```
public class ProtectedInternalModifier
{
    protected internal int myValue = 10;

    protected internal void ShowMessage()
    {
        Console.WriteLine("This is a protected internal method.");
    }

    protected internal virtual int GetValue() => myValue;
}

class TestProtectedInternalAccess
{
    public static void Run()
    {
        var pim = new ProtectedInternalModifier();
        pim.ShowMessage();
        Console.WriteLine($"Value: {pim.myValue}");
    }
}

class DerivedProtectedInternalClassSameAssembly : ProtectedInternalModifier
{
    protected internal override int GetValue() => 9;
}

Cross-assembly example (Consuming-REST-API/AccessModifiers/DerivedProtectedInternalModifier.cs):

class DerivedProtectedInternalModifier : ProtectedInternalModifier
{
    static void Main()
    {
        var baseObject = new ProtectedInternalModifier();
        var derivedObject = new DerivedProtectedInternalModifier();

        // Error: baseObject.myValue not accessible (not derived)
        // baseObject.myValue = 10;

        // OK: accessible in derived type
        derivedObject.myValue = 10;
    }
}

class DerivedClassDifferentAssembly : ProtectedInternalModifier
{
    protected override int GetValue() => 2; // override is protected (not internal) here
}
```
Key takeaway:
- In other assemblies, accessibility for overrides is effectively protected.
---

## 5. private protected
Accessible only within the containing assembly AND derived types.
Source: CSharpFeatures/AccessModifiers/PrivateProtectedModifier.cs

```
public class PrivateProtectedModifier
{
    private protected int myValue = 10;
}

public class DerivedPrivateProtectedModifier : PrivateProtectedModifier
{
    public static void Run()
    {
        var baseObject = new PrivateProtectedModifier();
        var derivedObject = new DerivedPrivateProtectedModifier();

        // Error: baseObject.myValue not accessible (not derived)
        // baseObject.myValue = 10;

        // OK: derived type within same assembly
        derivedObject.myValue = 20;
        Console.WriteLine($"DerivedPrivateProtectedModifier myValue: {derivedObject.myValue}");
    }
}

Cross-assembly example (Consuming-REST-API/AccessModifiers/DerivedPrivateProtectedModifier.cs)

public class DerivedPrivateProtectedModifier : PrivateProtectedModifier
{
    public static void Run()
    {
        var derivedObject = new DerivedPrivateProtectedModifier();
        // Error: myValue cannot be accessed in different assembly
        // derivedObject.myValue = 10;
    }
}
```
Key takeaway:
- Stricter than protected internal; requires both derived AND same assembly.
---

## 6. private
Accessible only within the containing type.
Source: CSharpFeatures/AccessModifiers/PrivateModifier.cs
```
class Employee
{
    private string _name = "James";
    private decimal _salary = 4000;

    public string GetEmployeeName()
    {
        return _name;
    }

    public void SetEmployeeName(string name)
    {
        _name = name;
    }

    public decimal GetEmployeeSalary()
    {
        return _salary;
    }

    public void SetEmployeeSalary(decimal salary)
    {
        _salary = salary;
    }
}

class PrivateTest
{
    public static void Run()
    {
        var e = new Employee();

        // The data members are inaccessible (private), so
        // they can't be accessed like this:
        //string n = e._name;
        //decimal s = e._salary;

        // '_name' is indirectly accessed via method:
        string name = e.GetEmployeeName();

        // '_salary' is indirectly accessed via property
        decimal salary = e.GetEmployeeSalary();

        Console.WriteLine($"Name: {name}, Salary: {salary}");   
    }
}
```
---

## 7. file (file-scoped type)
Visible only within the same source file.
Source: CSharpFeatures/AccessModifiers/FileModifier.cs and HiddenWidget.cs/FileModifier.cs
```
file class HiddenWidget
{
    public static void Run()
    {
        Console.WriteLine("HiddenWidget Run method executed.");
    }
}

// HiddenWidget.cs (separate type in same namespace, no conflict)
public class HiddenWidget
{
    public static void Run()
    {
        Console.WriteLine("HiddenWidget Run method executed.");
    }
}
```
Key takeaway:
- `file` types are limited to the file; they do not collide with public types of same name in other files.
---

## Guidelines
```
Expose only what consumers need
Use protected carefully (composition often preferred)
Use private protected for tight intra-assembly inheritance scenarios
Use file for file-local helpers
```
---

## Included Exercises/ReferencesCSharpFeatures/AccessModifiers/
```
- PublicClass.cs
- InternalClass.cs
- PrivateModifier.cs
- ProtectedModifier.cs
- ProtectedInternalModifier.cs
- PrivateProtectedModifier.cs
- FileModifier.cs
- HiddenWidget.cs

Consuming-REST-API/AccessModifiers/
- DerivedProtectedInternalModifier.cs
- DerivedPrivateProtectedModifier.cs
```