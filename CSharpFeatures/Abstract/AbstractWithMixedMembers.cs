namespace CSharpFeatures.Abstract
{
    public class AbstractWithMixedMembers
    {
        public static void Run()
        {
            // Cannot instantiate abstract class: Human v = new Human("Generic"); // Error!

            Human doctor = new Doctor("Alice");
            Human engineer = new SoftwareEngineer("Bob");
            doctor.Walk();

            // Using implemented methods from abstract class
            Console.WriteLine(doctor.GetInfo());

            // Using abstract methods implemented in derived class
            doctor.Job();

            engineer.Walk();
            Console.WriteLine(engineer.GetInfo());
            engineer.Job();
        }
    }

    public abstract class Human
    {
        protected string _name;

        // Constructor - implemented method in abstract class
        protected Human(string name)
        {
            _name = name;
        }

        // Implemented method - provides functionality that human vehicles share
        public virtual void Walk()
        {
            Console.WriteLine($"{_name} is walking.");
        }

        // Another implemented method
        public string GetInfo()
        {
            return $"Name: {_name}, Job: {JobTitle}";
        }

        // Abstract property - must be implemented by derived classes  
        public abstract string JobTitle { get; }

        // Abstract method - must be implemented by derived classes
        public abstract void Job();
    }

    public class Doctor : Human
    {
        public Doctor(string name) : base(name) { }

        // Implementation of abstract property
        public override string JobTitle => "Doctor";

        // Implementation of abstract method
        public override void Job()
        {
            Console.WriteLine($"{_name} is treating patients.");
        }
    }

    public class SoftwareEngineer : Human
    {
        public SoftwareEngineer(string name) : base(name) { }

        // Implementation of abstract property
        public override string JobTitle => "Software Engineer";

        // Implementation of abstract method
        public override void Job()
        {
            Console.WriteLine($"{_name} is writing code and designing systems.");
        }
    }
}
