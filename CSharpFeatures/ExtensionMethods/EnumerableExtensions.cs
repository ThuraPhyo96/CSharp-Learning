namespace CSharpFeatures.ExtensionMethods
{
    // extension method to perform an action on each element of an IEnumerable<T>
    internal static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

        public static void Run()
        {
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
            Console.WriteLine("Using ForEach extension method to print numbers:");
            numbers.ForEach(n => Console.WriteLine(n));
        }
    }
}
