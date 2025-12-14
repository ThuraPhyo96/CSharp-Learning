namespace CSharpFeatures.Constructors
{
    // Static constructor runs before static member access
    internal class StaticConstructorRunsBeforeFirstUse
    {
        static string AppName;

        static StaticConstructorRunsBeforeFirstUse()
        {
            AppName = "My Application";
            Console.WriteLine("Static constructor executed.");
        }

        public static void Run()
        {
            Console.WriteLine($"Welcome to {AppName}!");
        }
    }
}
