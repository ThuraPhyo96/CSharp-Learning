namespace CSharpFeatures.AccessModifiers
{
    // Assembly: CSharpFeatures.cs
    public class ProtectedInternalModifier
    {
        protected internal int myValue = 10;

        protected internal void ShowMessage()
        {
            Console.WriteLine("This is a protected internal method.");
        }

        protected internal virtual int GetValue()
        {
            return myValue;
        }
    }

    class TestProtectedInternalAccess
    {
        public static void Run()
        {
            ProtectedInternalModifier pim = new ProtectedInternalModifier();
            pim.ShowMessage(); // Accessible here

            pim.GetValue(); // Accessible here
            Console.WriteLine($"Value: {pim.myValue}");
        }
    }

    class DerivedProtectedInternalClassSameAssembly : ProtectedInternalModifier
    {
        // Override to return a different example value, accessibility modifiers remain the same.
        protected internal override int GetValue()
        {
            return 9;
        }

        public static void Run()
        {
            DerivedProtectedInternalClassSameAssembly pim = new DerivedProtectedInternalClassSameAssembly();

            pim.GetValue(); // Accessible here
            Console.WriteLine($"Value: {pim.myValue}");
        }
    }
}
