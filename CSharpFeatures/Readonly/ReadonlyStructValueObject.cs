namespace CSharpFeatures.Readonly
{
    // Immutable value object using readonly struct
    public readonly struct Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public double DistanceFromOrigin()
        {
            return Math.Sqrt(X * X + Y * Y);
        }
    }

    internal class ReadonlyStructValueObject
    {
        public static void Run()
        {
            Point p = new Point(3, 4);
            Console.WriteLine($"Point({p.X}, {p.Y}) is {p.DistanceFromOrigin()} units from the origin.");
        }
    }
}
