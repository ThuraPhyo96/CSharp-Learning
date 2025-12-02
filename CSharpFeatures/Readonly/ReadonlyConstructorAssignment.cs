namespace CSharpFeatures.Readonly
{
    internal class ReadonlyConstructorAssignment
    {
        public readonly string Name;
        public readonly DateTime DOB;

        public ReadonlyConstructorAssignment(string name, DateTime dob)
        {
            Name = name;
            DOB = dob;
        }

        public static void Run()
        {
            var person = new ReadonlyConstructorAssignment("Alice", new DateTime(1985, 5, 20));
            Console.WriteLine($"Name: {person.Name}, Date of Birth: {person.DOB.ToShortDateString()}");

            // This line will cause a compile-time error
            // person.Name = "Bob"; 
        }
    }
}
