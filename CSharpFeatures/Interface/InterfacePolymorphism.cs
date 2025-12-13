namespace CSharpFeatures.Interface
{
    // Interface references enable polymorphism
    public class InterfacePolymorphism
    {
        public static void Run()
        {
            IWorker manager = new Manager();
            IWorker developer = new Developer();
            manager.Work();    // Output: Managing team and projects.
            developer.Work();  // Output: Writing and debugging code.
        }
    }

    public interface IWorker
    {
        void Work();
    }

    public class Manager : IWorker
    {
        public void Work()
        {
            Console.WriteLine("Managing team and projects.");
        }
    }

    public class Developer : IWorker
    {
        public void Work()
        {
            Console.WriteLine("Writing and debugging code.");
        }
    }
}
