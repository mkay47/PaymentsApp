using CentralServiceAPI.web.Notification.Models;
using ServiceReference1;

namespace CentralServiceAPI.web.Notification.Mappings
{
    public class NotificationMapping : INotificationMapping
    {
        public MessageOutput MapNotificationOutput(SendEmailResponse sendEmailResponse)
        {
            return new MessageOutput
            {
                Response = sendEmailResponse.Response
            };
        }

        public MessageOutput MapNotificationOutput(SendSMSResponse sendSMSResponse)
        {
            return new MessageOutput
            {
                Response = sendSMSResponse.Response
            };
        }
    }
}