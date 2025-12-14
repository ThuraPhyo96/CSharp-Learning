namespace CSharpFeatures.ExtensionMethods
{
    // extension method to check if a DateTime is on a weekend
    internal static class DateTimeExtensions
    {
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        public static void Run()
        {
            DateTime today = DateTime.Now;
            Console.WriteLine($"{today.ToShortDateString()} is weekend: {today.IsWeekend()}");
            DateTime saturday = new DateTime(2024, 6, 8);
            Console.WriteLine($"{saturday.ToShortDateString()} is weekend: {saturday.IsWeekend()}");
        }
    }
}
