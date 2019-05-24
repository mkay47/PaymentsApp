using NotificationServiceAPI.Models;
using NotificationServiceAPI.Orchestration;

namespace NotificationServiceAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    //[AuthenticationInspectorBehavior]
    public class NotificationService : INotificationService
    {
        public SendEmailResponse SendEmail(SendEmailRequest request)
        {
            var notification = new NotificationOrchestration();

            return notification.SendEmail(request);
        }

        public SendSMSResponse SendSMS(SendSMSRequest request)
        {
            var notification = new NotificationOrchestration();

            return notification.SendSMS(request);
        }
    }
}