using CSharpFeatures.Abstract;
using CSharpFeatures.AccessModifiers;
using CSharpFeatures.Generics;
using CSharpFeatures.Interface;
using CSharpFeatures.LINQs;
using System.Globalization;

class Program
{
    public static void Main()
    {
        // Uncomment any demo to run:

        //BeforeGenerics();
        //GenericMethodExample();
        //ReferenceTypeConstraintExample();
        //ValueTypeConstraintExample();
        //ConstructorConstraintExample();
        //ConversionConstraintExample();
        //ExtensionSmampleDemo();

        //LINQDemo linqDemo = new LINQDemo();
        //linqDemo.ShowLINQExamples();

        // Readonly demos
        //BasicReadonlyUsage.Run();
        //ReadonlyConstructorAssignment.Run();
        //ReadonlyVsConstComparison.Run();
        //ReadonlyReferenceMutation.Run();
        //ReadonlyStructValueObject.Run();
        //ReadonlyDependencyInjection.Run();  

        // Sealed demos
        //BasicSealedClassUsage.Run();
        //BasicSealedMethodUsage.Run();
        //CombinedSealedWithPolymorphism.Run();
        //SealedUtilityClass.Run();
        //SealedForProtection.Run();

        // Access Modifiers demos
        //PublicClass.Run();
        //InternalClass.Run();
        //PrivateTest.Run();
        //DerivedProtected.Run();
        //TestProtectedInternalAccess.Run();
        //DerivedProtectedInternalClassSameAssembly.Run();
        //DerivedPrivateProtectedModifier.Run();
        //HiddenWidget.Run();

        // Abstract demos
        //AbstractWithMixedMembers.Run();

        // Interface demos
        //MultipleInterfaces.Run();
        //InterfacePolymorphism.Run();
        //InterfaceWithDI.Run();
    }

    private static void BeforeGenerics()
    {
        CollectionsBeforeGenerics collectionsBeforeGenerics = new CollectionsBeforeGenerics();
        var namesArray = collectionsBeforeGenerics.GetNames();
        collectionsBeforeGenerics.PrintNames(namesArray);

        var namesArrayList = collectionsBeforeGenerics.GetNamesUsingArrayList();
        collectionsBeforeGenerics.PrintNames(namesArrayList);

        var namesStringCollection = collectionsBeforeGenerics.GetNamesUsingStringCollection();
        collectionsBeforeGenerics.PrintNames(namesStringCollection);
    }

    private static void GenericMethodExample()
    {
        List<string> names = new List<string>() { "Welcome", "To", "Da Nang, ", "VietNam" };
        GenericHelper.PrintNames(names);

        List<int> namesInt = new List<int>() { 1, 2, 3, 4 };
        GenericHelper.PrintNames(namesInt);
    }

    private static void ReferenceTypeConstraintExample()
    {
        // Valid: Order is a reference type
        ReferenceTypeConstraintDemo<Order> referenceTypeConstraintDemo = new ReferenceTypeConstraintDemo<Order>();
        referenceTypeConstraintDemo.AddItem("ORD123", new Order() { Id = 1, Status = "Pending" });

        Order? order = referenceTypeConstraintDemo.GetItem("ORD123");

        if (order != null)
        {
            Console.WriteLine($"Order ID: {order.Id}, Status: {order.Status}");
        }

        // Valid: string is a reference type
        ReferenceTypeConstraintDemo<string> stringDemo = new ReferenceTypeConstraintDemo<string>();
        stringDemo.AddItem("Key1", "Hello World");

        string? stringValue = stringDemo.GetItem("Key1");
        if (stringValue != null)
        {
            Console.WriteLine($"String Value: {stringValue}");
        }

        // Invalid: int is a value type
        // ReferenceTypeConstraintDemo<int> intDemo = new ReferenceTypeConstraintDemo<int>(); // Compile-time error: 'int' must be a reference type
    }

    private static void ValueTypeConstraintExample()
    {
        // Valid: int is a primitive struct
        ValueTypeConstraintDemo<int> primitiveStructConstraintDemo = new ValueTypeConstraintDemo<int>(5);
        if (primitiveStructConstraintDemo.HasValue())
        {
            Console.WriteLine($"Primitive Struct: {primitiveStructConstraintDemo.Value()}");
        }

        // Valid : Guid is a system struct
        ValueTypeConstraintDemo<Guid> systemStructConstraintDemo = new ValueTypeConstraintDemo<Guid>(Guid.NewGuid());
        if (systemStructConstraintDemo.HasValue())
        {
            Console.WriteLine($"System Struct: {systemStructConstraintDemo.Value()}");
        }

        // Valid: Enum is a special kind of struct
        ValueTypeConstraintDemo<FileMode> enumConstraintDemo = new ValueTypeConstraintDemo<FileMode>(FileMode.Append);
        if (enumConstraintDemo.HasValue())
        {
            Console.WriteLine($"Enum: {enumConstraintDemo.Value()}");
        }

        // Invalid: string is a reference type
        // ValueTypeConstraintDemo<string> referenceTypeContraintDemo = new ValueTypeConstraintDemo<string>("Test");

        // Invalid: int[] (Array) is a reference type
        //ValueTypeConstraintDemo<int[]> referenceTypeContraintDemo = new ValueTypeConstraintDemo<int[]>(new int[] { 1, 2, 3 });

        // Invalid: Nullable<int> is nuallable value type
        //ValueTypeConstraintDemo<Nullable<int>> referenceTypeContraintDemo = new ValueTypeConstraintDemo<Nullable<int>>(5);
    }

    private static void ConstructorConstraintExample()
    {
        // This call is valid because MyCreatableClass has a public parameterless constructor.
        SampleConstructorConstraintClass sampleConstructorConstraint = ConstructorConstraintDemo.CreateInstance<SampleConstructorConstraintClass>();
        Console.WriteLine(sampleConstructorConstraint.Message);

        // This call is valid because SampleConstructorConstraintClassOne has an implicit default constructor.
        SampleConstructorConstraintClassOne sampleConstructorConstraintOne = ConstructorConstraintDemo.CreateInstance<SampleConstructorConstraintClassOne>();
        Console.WriteLine(sampleConstructorConstraintOne.Message);

        // This will also work since we are directly using the 'new' keyword.
        SampleConstructorConstraintClass sampleConstructorConstraintClass = new SampleConstructorConstraintClass();
        Console.WriteLine(sampleConstructorConstraintClass.Message);
    }

    private static void ConversionConstraintExample()
    {
        // 1. Create a List of a type that implements IFormattable (e.g., decimal or int).
        // Decimal implements IFormattable, satisfying the conversion constraint
        List<decimal> prices = new List<decimal> { 1000.99m, 1290.99m, 999.99m };

        // 2. Client calls the constrained generic method.
        // The compiler infers T as 'decimal', and since decimal : IFormattable is true, 
        // the method call is valid.
        Console.WriteLine("--- Normal Output ---");
        ConversionConstraintDemo.PrintItems(prices);

        // 1. U.S. Dollar (USD)
        CultureInfo usCulture = CultureInfo.GetCultureInfo("en-US");
        Console.WriteLine("--- USD Output ---");
        ConversionConstraintDemo.PrintWithCurrency(prices, usCulture);
        // Expected output example: $1,000.00, $50,000.75

        // 2. Singapore Dollar (SGD)
        CultureInfo sgCulture = CultureInfo.GetCultureInfo("en-SG");
        Console.WriteLine("--- SGD Output ---");
        ConversionConstraintDemo.PrintWithCurrency(prices, sgCulture);
        // Expected output example: S$1,000.00, S$50,000.75
    }

    private static void ExtensionSmampleDemo()
    {
        List<Product> products = new List<Product>()
        {
            new Product() { Name = "Laptop", Price = 1200.50m },
            new Product() { Name = "Smartphone", Price = 800.75m },
            new Product() { Name = "Tablet", Price = 450.00m }
        };

        // Calling the extension method as if it were an instance method
        decimal totalPrice = products.TotalPrice();
        Console.WriteLine($"Total Price: {totalPrice:C2}");
    }
}