namespace CSharpFeatures.AccessModifiers
{
    // Assembly A (project A)
    public class PublicClass
    {
        public string Message() => "Hello from PublicClass!";

        public static void Run()
        {
            PublicClass pc = new PublicClass();
            Console.WriteLine(pc.Message()); // accessing public method
        }
    }
}
