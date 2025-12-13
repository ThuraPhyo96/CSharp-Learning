namespace CSharpFeatures.Interface
{
    internal class MultipleInterfaces
    {
        public static void Run()
        {
            Engineer engineer = new Engineer();
            engineer.Walk();
            engineer.Sleep();
        }
    }

    public interface IWalker
    {
        void Walk();
    }

    public interface ISleeper
    {
        void Sleep();
    }

    // Classes can implement multiple interfaces
    public class Engineer : IWalker, ISleeper
    {
        public void Walk()
        {
            Console.WriteLine("Engineer walking");
        }

        public void Sleep()
        {
            Console.WriteLine("Engineer sleeping");
        }
    }
}
