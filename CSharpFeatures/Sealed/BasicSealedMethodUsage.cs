namespace CSharpFeatures.Sealed
{
    internal class BasicSealedMethodUsage
    {
        public class Vehicle
        {
            public virtual void StartEngine()
            {
                Console.WriteLine("Engine started.");
            }
        }

        public class Car : Vehicle
        {
            public sealed override void StartEngine()
            {
                Console.WriteLine("Car engine started with a roar!");
            }
        }

        public class SportsCar : Car
        {
            // This will cause a compile-time error because StartEngine is sealed in Car.
            // public override void StartEngine()
            // {
            //     Console.WriteLine("Sports car engine started with extra power!");
            // }
        }

        public static void Run()
        {
            Vehicle myVehicle = new Vehicle();
            myVehicle.StartEngine(); // Output: Engine started.
            Vehicle myCar = new Car();
            myCar.StartEngine(); // Output: Car engine started with a roar!
            // Vehicle mySportsCar = new SportsCar();
            // mySportsCar.StartEngine(); // This line would cause a compile-time error if StartEngine were not sealed.
        }
    }
}
