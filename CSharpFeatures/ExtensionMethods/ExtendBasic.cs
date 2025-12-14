namespace CSharpFeatures.ExtensionMethods
{
    // a simple extension method to count words in a string
    internal static class ExtendBasic
    {
        public static int WordCount(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return 0;
            }
            string[] words = str.Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        public static void Run()
        {
            string example = "Hello, this is an example string.";
            int count = example.WordCount();
            Console.WriteLine($"The string contains {count} words.");
        }
    }
}
