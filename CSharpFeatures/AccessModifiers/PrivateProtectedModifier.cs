namespace CSharpFeatures.AccessModifiers
{
    // Assembly: CSharpFeatures.cs
    public class PrivateProtectedModifier
    {
        private protected int myValue = 10;
    }

    public class DerivedPrivateProtectedModifier : PrivateProtectedModifier
    {
        public static void Run()
        {
            var baseObject = new PrivateProtectedModifier();
            var derivedObject = new DerivedPrivateProtectedModifier();
            // Error CS1540, because myValue can only be accessed by
            // classes derived from BaseClass within the same assembly.
            // baseObject.myValue = 10;

            // OK, because this class derives from BaseClass within the same assembly.
            derivedObject.myValue = 20;

            Console.WriteLine($"DerivedPrivateProtectedModifier myValue: {derivedObject.myValue}"); 
        }
    }
}
