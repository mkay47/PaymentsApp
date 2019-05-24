using NotificationServiceAPI.Extensions;
using NotificationServiceAPI.Models;
using System.ServiceModel;

namespace NotificationServiceAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.

    [ServiceContract(Name = "NotificationService")]
    public interface INotificationService
    {
        // TODO: Add your service operations here
        [CustomEmailHeaderBehaviour]
        [OperationContract(Name = "Email", ProtectionLevel = System.Net.Security.ProtectionLevel.None)]
        SendEmailResponse SendEmail(SendEmailRequest request);

        [CustomSMSHeaderBehaviour]
        [OperationContract(Name = "SMS", ProtectionLevel = System.Net.Security.ProtectionLevel.None)]
        SendSMSResponse SendSMS(SendSMSRequest request);
    }
}