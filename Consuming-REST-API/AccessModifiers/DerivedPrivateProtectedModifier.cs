using CSharpFeatures.AccessModifiers;

namespace Consuming_REST_API.AccessModifiers
{
    // Assembly: Consuming_REST_API.cs
    // Compile with: /reference:CSharpFeatures.dll
    public class DerivedPrivateProtectedModifier : PrivateProtectedModifier
    {
        public static void Run()
        {
            var derivedObject = new DerivedPrivateProtectedModifier();
            // Error CS0122, because myValue can only be
            // accessed by types in Assembly1
            //derivedObject.myValue = 10;
        }
    }
}
