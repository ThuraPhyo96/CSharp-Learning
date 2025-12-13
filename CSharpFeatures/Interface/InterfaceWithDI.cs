namespace CSharpFeatures.Interface
{
    // Code depends on interfaces, not concrete classes
    // Enables testing, swapping implementations
    internal class InterfaceWithDI
    {
       public static void Run()
        {
            IMessageService emailService = new EmailService();
            Notification notification = new Notification(emailService);
            notification.Notify();

            IMessageService smsService = new SMSService();
            Notification smsnotification = new Notification(smsService);
            smsnotification.Notify();

            IMessageService pushNotiService = new PushNotificationService();
            Notification pushNotification = new Notification(pushNotiService);
            pushNotification.Notify();
        }   
    }

    public interface IMessageService
    {
        void Send(string message);
    }

    public class EmailService : IMessageService
    {
        public void Send(string message)
        {
            Console.WriteLine($"Email sent: {message}");
        }
    }

    public class SMSService : IMessageService
    {
        public void Send(string message)
        {
            Console.WriteLine($"SMS sent: {message}");
        }
    }

    public class PushNotificationService : IMessageService
    {
        public void Send(string message)
        {
            Console.WriteLine($"Push notification sent: {message}");
        }
    }

    public class Notification
    {
        private readonly IMessageService _messageService;

        public Notification(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public void Notify()
        {
            _messageService.Send("Hello!");
        }
    }
}
