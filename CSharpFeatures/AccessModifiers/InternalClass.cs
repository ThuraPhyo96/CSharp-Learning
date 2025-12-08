namespace CSharpFeatures.AccessModifiers
{
    internal class InternalClass
    {
        internal void DisplayMessage()
        {
            Console.WriteLine("Hello from InternalClass!");
        }

        public static void Run()
        {
            InternalClass ic = new InternalClass();
            ic.DisplayMessage(); // accessing internal method
        }
    }
}
