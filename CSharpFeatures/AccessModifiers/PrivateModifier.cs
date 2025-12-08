namespace CSharpFeatures.AccessModifiers
{
    class PrivateModifier
    {
        private int _privateField = 42;
        private void PrivateMethod()
        {
            Console.WriteLine("This is a private method.");
        }
    }

    class Employee
    {
        private string _name = "James";
        private decimal _salary = 4000;

        public string GetEmployeeName()
        {
            return _name;
        }

        public void SetEmployeeName(string name)
        {
            _name = name;
        }

        public decimal GetEmployeeSalary()
        {
            return _salary;
        }

        public void SetEmployeeSalary(decimal salary)
        {
            _salary = salary;
        }
    }

    class PrivateTest
    {
        public static void Run()
        {
            var e = new Employee();

            // The data members are inaccessible (private), so
            // they can't be accessed like this:
            //string n = e._name;
            //decimal s = e._salary;

            // '_name' is indirectly accessed via method:
            string name = e.GetEmployeeName();

            // '_salary' is indirectly accessed via property
            decimal salary = e.GetEmployeeSalary();

            Console.WriteLine($"Name: {name}, Salary: {salary}");   
        }
    }
}
