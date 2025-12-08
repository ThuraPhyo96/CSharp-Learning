namespace CSharpFeatures.AccessModifiers
{
    class ProtectedModifier
    {
        protected string name = "Protected Name";
    }

    class DerivedProtected : ProtectedModifier
    {
        public static void Run()
        {
            var baseObject = new ProtectedModifier();
            var derivedObject = new DerivedProtected();

            // Error CS1540, because name can only be accessed through
            // the derived class type, not through the base class type.
            // baseObject.name = "New Name"; 

            Console.WriteLine($"Before accessible: {derivedObject.name}");

            // OK, because this class derives from BaseClass.
            derivedObject.name = "Updated from derived class";

            Console.WriteLine($"After accessible: {derivedObject.name}");
        }
    }
}
