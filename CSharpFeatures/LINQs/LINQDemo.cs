namespace CSharpFeatures.LINQs
{
    internal class LINQDemo
    {
        private readonly List<Product> _products = new List<Product>
        {
            new Product { Name = "Product A", Price = 10.5m, StockCount = 5 },
            new Product { Name = "Product B", Price = 20.0m, StockCount = 0 },
            new Product { Name = "Product C", Price = 15.75m, StockCount = 10 },
            new Product { Name = "Product D", Price = 30.0m, StockCount = 3 },
            new Product { Name = "Product E", Price = 25.5m, StockCount = 8 }
        };

        public void ShowLINQExamples()
        {
            // 1. LINQ Query Expression Syntax: Declarative, SQL-like syntax 
            var products =
                from product in _products
                where product.StockCount > 0 // Filter condition
                orderby product.Price descending // Ordering condition
                select new { product.Name, product.Price, product.StockCount }; // Projection using Anonymous Type

            foreach (var product in products)
            {
                Console.WriteLine("Name: " + product.Name);
                Console.WriteLine("Price: " + product.Price);
                Console.WriteLine("Stock Count: " + product.StockCount);
                Console.WriteLine();
            }

            // 2. Query Translation (Conceptual): The compiler converts the Query Expression 
            // into Method Syntax using Extension Methods and Lambda Expressions. 

            /* Conceptual translation:
            var products = _products
                .Where(product => product.StockCount > 0) // Lambda Expression 
                .OrderByDescending(product => product.Price) // Extension Method
                .Select(product => new { product.Name, product.Price }); // Anonymous Type 
            */

            // 3. Execution (via Expression Trees): Since '_products' implements 
            // IQueryable<T>, the query is represented as an Expression Tree

            // Example 1: Filtering
            var stockCountProducts = _products.Where(n => n.StockCount == 0);
            foreach (var product in stockCountProducts)
            {
                Console.WriteLine("LINQ Filtering: " + product.Name);
            }

            // Example 2: Projection
            var projectionProducts = _products.OrderBy(p => p.Name).Select(n => n.Name);
            Console.WriteLine("LINQ Projection: " + string.Join(", ", projectionProducts));

            // Example 3: Sum of all prices
            var sum = _products.Sum(x => x.Price);
            Console.WriteLine("Sum of prices: " + sum);

            // Example 4: Average of all prices
            var average = _products.Average(x => x.Price);
            Console.WriteLine("Average of prices: " + average);
        }
    }
}
