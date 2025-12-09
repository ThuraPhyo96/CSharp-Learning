using CSharpFeatures.AccessModifiers;

namespace Consuming_REST_API.AccessModifiers
{
    // Assembly: Consuming_REST_API.cs
    // Compile with: /reference:CSharpFeatures.dll
    class DerivedProtectedInternalModifier : ProtectedInternalModifier
    {
        static void Main()
        {
            var baseObject = new ProtectedInternalModifier();
            var derivedObject = new DerivedProtectedInternalModifier();

            // Error CS1540, because myValue can only be accessed by
            // classes derived from BaseClass.
            // baseObject.myValue = 10;

            // OK, because this class derives from BaseClass.
            derivedObject.myValue = 10;
        }
    }

    // Assembly: Consuming_REST_API.cs
    // Compile with: /reference:CSharpFeatures.dll
    class DerivedClassDifferentAssembly : ProtectedInternalModifier
    {
        // Override to return a different example value, since this override
        // method is defined in another assembly, the accessibility modifiers
        // are only protected, instead of protected internal.
        protected override int GetValue()
        {
            return 2;
        }
    }
}
