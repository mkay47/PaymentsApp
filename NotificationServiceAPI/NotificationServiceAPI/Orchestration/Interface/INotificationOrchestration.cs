using NotificationServiceAPI.Models;

namespace NotificationServiceAPI.Orchestration
{
    public interface INotificationOrchestration
    {
        SendEmailResponse SendEmail(SendEmailRequest request);

        SendSMSResponse SendSMS(SendSMSRequest request);
    }
}