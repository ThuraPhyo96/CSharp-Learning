namespace CSharpFeatures.Interface
{
    public class InterfaceImplementation
    {
    }

    public interface IJob
    {
        protected string JobTitle { get; }
        void PerformJob();
    }

    // This should NOT compile
    //public class Engineer : IJob
    //{

    //}

    // all interface members must be implemented.
    public class Teacher : IJob
    {
        public string JobTitle => "Teacher";
        public void PerformJob()
        {
            Console.WriteLine("Teaching students.");
        }
    }
}
