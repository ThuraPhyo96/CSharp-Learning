using System.Globalization;

namespace CSharpFeatures.Generics
{
    internal class ConversionConstraintDemo
    {
        // Constrained method definition: T must implement IFormattable.
        public static void PrintItems<T>(List<T> items) where T : IFormattable
        {
            // Specify the invariant culture as the format provider. 
            // This culture is suitable for machine-to-machine communication 
            // but ensures predictable formatting behavior, using the period 
            // as the decimal separator and the comma as the grouping separator 
            // for standard numeric formats. 
            CultureInfo culture = CultureInfo.InvariantCulture;

            // Specify the "N2" Numeric format string.
            // 'N' stands for Numeric and automatically includes group separators 
            // (thousands separators) and defaults to two decimal places (N2)
            string numericFormat = "N2";

            foreach (var item in items)
            {
                // This call is enabled by the conversion constraint (T : IFormattable)
                // It applies the numeric format using the invariant culture settings.
                Console.WriteLine(item.ToString(numericFormat, culture));
            }
        }

        public static void PrintWithCurrency<T>(List<T> items, CultureInfo cultureInfo) where T : IFormattable
        {
            // Specify the "C2" Currency format string.
            // 'C' stands for Currency and automatically includes the currency symbol 
            // and defaults to two decimal places (C2)
            string currencyFormat = "C2";

            foreach (var item in items)
            {
                // This call is enabled by the conversion constraint (T : IFormattable)
                // It applies the currency format using the specified culture settings.
                Console.WriteLine(item.ToString(currencyFormat, cultureInfo));
            }
        }
    }
}