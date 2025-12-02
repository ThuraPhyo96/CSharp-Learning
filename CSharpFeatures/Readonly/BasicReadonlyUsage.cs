namespace CSharpFeatures.Readonly
{
    internal class BasicReadonlyUsage
    {
        // Readonly fields can only be assigned at declaration or inside constructor
        public readonly string Name = "James";
        public readonly DateTime DOB;
        
        public BasicReadonlyUsage()
        {
            DOB = new DateTime(1990, 8, 11);
        }

        public static void Run()
        {
            var person = new BasicReadonlyUsage();
            Console.WriteLine($"Name: {person.Name}, Date of Birth: {person.DOB.ToShortDateString()}");
        }
    }
}
