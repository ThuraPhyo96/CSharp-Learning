namespace CSharpFeatures.LINQs
{
    internal static class ExtensionMethodDemo
    {
        // The first parameter is the extended type, prefixed with 'this'.
        public static decimal TotalPrice(this IEnumerable<Product?> products)
        {
            decimal total = 0;
            foreach (var product in products)
            {
                // Safely adds the price, treating null price as 0.
                total += product?.Price ?? 0;
            }
            return total;
        }
    }

    public class Product
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int StockCount { get; set; }
    }
}
