namespace CSharpFeatures.Constructors
{
    // Static constructors cannot have parameters.
    internal class ParametersNotAllowedInStaticConstructor
    {
        // This will cause a compile-time error
        //static ParametersNotAllowedInStaticConstructor(string name)
        //{
        //    Console.WriteLine("Static constructor called.");
        //}

        public ParametersNotAllowedInStaticConstructor(string name)
        {
            Console.WriteLine($"Instance constructor called with name: {name}");
        }
    }
}
