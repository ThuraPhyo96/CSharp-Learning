namespace CSharpFeatures.Sealed
{
    internal class SealedForProtection
    {
        public class PaymentProcessor
        {
            public virtual void ProcessPayment(decimal amount)
            {
                Console.WriteLine($"Processing payment of ${amount}");
            }
        }

        public class CreditCardPaymentProcessor : PaymentProcessor
        {
            public sealed override void ProcessPayment(decimal amount)
            {
                if (amount <= 0)
                {
                    Console.WriteLine("Invalid payment amount.");
                    return;
                }
                Console.WriteLine($"Credit Card processing payment of ${amount}");
            }
        }

        public class AdvancedCreditCardPaymentProcessor : CreditCardPaymentProcessor
        {
            // This will cause a compile-time error because ProcessPayment is sealed in CreditCardPaymentProcessor.
            //public override void ProcessPayment(decimal amount)
            //{
            //    Console.WriteLine($"Advanced processing for credit card payment of ${amount}");
            //}
        }

        public static void Run()
        {
            PaymentProcessor processor = new CreditCardPaymentProcessor();
            processor.ProcessPayment(150.00m);
            processor.ProcessPayment(-20.00m);
        }
    }
}
