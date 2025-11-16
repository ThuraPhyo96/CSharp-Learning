namespace CSharpFeatures.Generics
{
    // T must be a reference type (class, interface, or delegate)
    internal class ReferenceTypeConstraintDemo<T> where T : class
    {
        private Dictionary<string, T> items = new Dictionary<string, T>();

        public void AddItem(string key, T item)
        {
            // The 'class' constraint allows T to be safely assigned/checked against null
            if (item != null)
            {
                items[key] = item;
            }
        }

        public T? GetItem(string key)
        {
            // Reference types can return null if not found
            return items.GetValueOrDefault(key);
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public string? Status { get; set; }
    }
}
