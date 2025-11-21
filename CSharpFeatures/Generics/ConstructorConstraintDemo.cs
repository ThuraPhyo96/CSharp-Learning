namespace CSharpFeatures.Generics
{
    internal class ConstructorConstraintDemo
    {
        // Define a method that requires the new() constraint on type T
        public static T CreateInstance<T>() where T : new()
        {
            return new T();
        }
    }

    // Define a class that meets the constraint (has a public parameterless constructor)
    public class SampleConstructorConstraintClass
    {
        public SampleConstructorConstraintClass() { }
        public string Message { get; set; } = "Public Parameterless Constructor!";
    }

    // Default constructor is implicit
    public class SampleConstructorConstraintClassOne
    {
        public string Message { get; set; } = "Default Constructor!";
    }
}
