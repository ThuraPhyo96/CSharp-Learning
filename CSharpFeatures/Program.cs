using CSharpFeatures.Generics;

class Program
{
    public static void Main()
    {
        //BeforeGenerics();
        //GenericMethodExample();
        ReferenceTypeConstraintExample();
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
}