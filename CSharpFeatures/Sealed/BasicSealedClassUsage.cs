namespace CSharpFeatures.Sealed
{
    internal class BasicSealedClassUsage
    {
        public sealed class BankAccount
        {
            public void PrintBalance()
            {
                Console.WriteLine("Your balance is $1,000.");
            }
        }

        // This will cause a compile-time error because BankAccount is sealed.
        //public class SavingsAccount : BankAccount 
        //{
        //    // public void PrintInterestRate()
        //    // {
        //    //     Console.WriteLine("The interest rate is 5%.");
        //    // }
        //}

        public static void Run()
        {
            BankAccount account = new BankAccount();
            account.PrintBalance();
        }
    }
}
