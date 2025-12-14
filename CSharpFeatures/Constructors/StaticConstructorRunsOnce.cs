namespace CSharpFeatures.Constructors
{
    // Static constructor runs once; instance constructor runs per object.
    public class StaticConstructorRunsOnce
    {
        static StaticConstructorRunsOnce()
        {
            Console.WriteLine("Static constructor called");
        }

        public StaticConstructorRunsOnce()
        {
            Console.WriteLine("Instance constructor called");
        }

        public static void Run()
        {
            var test1 = new StaticConstructorRunsOnce();
            var test2 = new StaticConstructorRunsOnce();
        }
    }
}
