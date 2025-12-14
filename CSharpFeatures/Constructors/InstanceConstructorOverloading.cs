namespace CSharpFeatures.Constructors
{
    // Instance constructors support overloading.
    internal class InstanceConstructorOverloading
    {
        public InstanceConstructorOverloading(string name)
        {
            Console.WriteLine($"Instance constructor called with name: {name}");
        }

        public InstanceConstructorOverloading(string name, DateTime dob)
        {
            Console.WriteLine($"Instance constructor called with name: {name} and date of birth: {dob.ToShortDateString()}");
        }

        public static void Run()
        {
            var instance1 = new InstanceConstructorOverloading("Alice");
            var instance2 = new InstanceConstructorOverloading("Bob", new DateTime(1990, 5, 15));
        }
    }
}
