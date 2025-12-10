namespace CSharpFeatures.AccessModifiers
{
    // Doesn't conflict with HiddenWidget
    // declared in FileModifier.cs
    public class HiddenWidget
    {
        public static void Run()
        {
            // omitted
            Console.WriteLine("HiddenWidget Run method executed.");
        }
    }
}
