namespace CSharpFeatures.Generics
{
    internal class GenericHelper
    {
        // Generic method works for any type T — int, string, etc.
        public static void PrintNames<T>(List<T> names)
        {
            foreach (T name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}
