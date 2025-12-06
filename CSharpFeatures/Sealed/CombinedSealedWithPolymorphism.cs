namespace CSharpFeatures.Sealed
{
    internal class CombinedSealedWithPolymorphism
    {
        public class Animal
        {
            public virtual void Speak()
            {
                Console.WriteLine("The animal makes a sound.");
            }
        }

        public class Dog : Animal
        {
            public sealed override void Speak()
            {
                Console.WriteLine("The dog barks.");
            }
        }

        public class Bulldog : Dog
        {
            // This will cause a compile-time error because Speak is sealed in Dog.
            // public override void Speak()
            // {
            //     Console.WriteLine("The bulldog growls.");
            // }
        }

        public static void Run()
        {
            Animal myAnimal = new Animal();
            myAnimal.Speak(); // Output: The animal makes a sound.
            Animal myDog = new Dog();
            myDog.Speak(); // Output: The dog barks.
            // Animal myBulldog = new Bulldog();
            // myBulldog.Speak(); // This line would cause a compile-time error if Speak were not sealed.
        }
    }
}
