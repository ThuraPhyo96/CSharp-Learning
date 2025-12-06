namespace CSharpFeatures.Sealed
{
    internal class SealedUtilityClass
    {
        public sealed class MathUtilities
        {
            public static int Add(int a, int b)
            {
                return a + b;
            }
            public static int Subtract(int a, int b)
            {
                return a - b;
            }
        }

        // This will cause a compile-time error because MathUtilities is sealed.
        //class MyMathUtilities : MathUtilities
        //{
        //    // public static int Multiply(int a, int b)
        //    // {
        //    //     return a * b;
        //    // }
        //}

        public static void Run()
        {
            int sum = MathUtilities.Add(5, 3);
            int difference = MathUtilities.Subtract(10, 4);
            Console.WriteLine($"Sum: {sum}, Difference: {difference}");
        }
    }
}
