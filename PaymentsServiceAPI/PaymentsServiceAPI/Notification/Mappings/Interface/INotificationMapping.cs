using CentralServiceAPI.web.Notification.Models;
using ServiceReference1;

namespace CentralServiceAPI.web.Notification.Mappings
{
    public interface INotificationMapping
    {
        MessageOutput MapNotificationOutput(SendEmailResponse sendEmailResponse);

        MessageOutput MapNotificationOutput(SendSMSResponse sendSMSResponse);
    }
}