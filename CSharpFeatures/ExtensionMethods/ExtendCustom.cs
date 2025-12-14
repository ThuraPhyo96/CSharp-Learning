namespace CSharpFeatures.ExtensionMethods
{
    public class User
    {
        public string? Name { get; set; }

        // Instance method is highter priority than extension method
        // so this method will be called instead of the extension method
        //public bool HasValidName()
        //{
        //    return !string.IsNullOrWhiteSpace(this.Name);
        //}
    }

    // extension method to check if a User has a valid (non-empty) name
    internal static class ExtendCustom
    {
        public static bool HasValidName(this User user)
        {
            return !string.IsNullOrWhiteSpace(user?.Name);
        }

        public static void Run()
        {
            User user1 = new User { Name = "Alice" };
            User user2 = new User { Name = "" };
            User user3 = null;
            Console.WriteLine($"User1 has valid name: {user1.HasValidName()}"); // True
            Console.WriteLine($"User2 has valid name: {user2.HasValidName()}"); // False
            Console.WriteLine($"User3 has valid name: {user3?.HasValidName()}"); 
        }
    }
}
