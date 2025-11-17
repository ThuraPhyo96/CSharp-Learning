namespace CSharpFeatures.Generics
{
    // The constraint ensures T is a non-nullable value type.
    internal class ValueTypeConstraintDemo<T> where T : struct
    {
        private readonly T _value;
        private readonly bool _hasValue = false;

        // Constructor creates a value where HasValue is true.
        public ValueTypeConstraintDemo(T value)
        {
            _value = value;
            _hasValue = true;
        }

        // Property to check whether there’s a real value.
        public bool HasValue()
        {
            return _hasValue;
        }

        // Access to the encapsulated value.
        public T? Value()
        {
            if (_hasValue)
            {
                return _value;
            }

            throw new InvalidOperationException();
        }
    }
}
