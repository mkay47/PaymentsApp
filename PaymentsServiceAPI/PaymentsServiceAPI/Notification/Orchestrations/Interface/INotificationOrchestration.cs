using CentralServiceAPI.web.Notification.Models;

namespace CentralServiceAPI.web.Notification.Orchestrations
{
    public interface INotificationOrchestration
    {
        MessageOutput SendEmail(MessageInput messageInput);

        MessageOutput SendSMS(MessageInput messageInput);
    }
}