namespace CSharpFeatures.Readonly
{
    internal class ReadonlyVsConstComparison
    {
        public const string AppName = "MyApplication"; // Compile-time constant
        public static readonly DateTime LaunchDate =  DateTime.Now; // Runtime constant

        public static void Run()
        {
            Console.WriteLine($"Comparing const vs readonly:");
            Console.WriteLine($"App Name (const): {AppName}");
            Console.WriteLine($"Launch Date (readonly): {LaunchDate}");

            Thread.Sleep(2000); // Simulate some delay

            Console.WriteLine("\nAfter delay:");
            Console.WriteLine($"App Name after delay (const): {AppName}");  // stays same
            Console.WriteLine($"Launch Date after delay (readonly): {LaunchDate}");  // stays same, not recalculated
        }
    }
}
