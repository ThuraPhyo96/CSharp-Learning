namespace CSharpFeatures.Readonly
{
    internal class ReadonlyReferenceMutation
    {
        public readonly List<string> Fruits = new List<string> { "Apple", "Orange" };

        public static void Run()
        {
            var demo = new ReadonlyReferenceMutation();
            // Modifying the contents of the readonly List
            demo.Fruits.Add("Banana");
            demo.Fruits.Remove("Apple");
            Console.WriteLine("Fruits List after mutation:");
            foreach (var fruit in demo.Fruits)
            {
                Console.WriteLine(fruit);
            }
            // Not allowed: reassigning the list reference
            // demo.Fruits = new List<string>(); // compile-time error
        }
    }
}
