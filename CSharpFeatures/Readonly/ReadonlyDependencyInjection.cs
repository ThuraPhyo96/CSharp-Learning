namespace CSharpFeatures.Readonly
{
    public interface ILogService
    {
        void Log(string message);
    }

    public class ConsoleLoggerService : ILogService
    {
        public void Log(string message)
        {
            Console.WriteLine($"Log: {message}");
        }
    }

    public class UserService
    {
        // Readonly to prevent reassignment — DI best practice
        private readonly ILogService _logService;
        public UserService(ILogService logService)
        {
            _logService = logService;
        }

        public void Create(string username)
        {
            _logService.Log($"User '{username}' has been created.");
        }
    }

    internal class ReadonlyDependencyInjection
    {
        public static void Run()
        {
            ILogService logger = new ConsoleLoggerService();
            UserService userService = new UserService(logger);
            userService.Create("Steve Smith");

            // Compile-time error: cannot reassign readonly field
            // userService._logService =  new ConsoleLoggerService(); 
        }
    }
}
