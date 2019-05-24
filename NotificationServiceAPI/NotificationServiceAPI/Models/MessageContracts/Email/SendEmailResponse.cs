using System.ServiceModel;

namespace NotificationServiceAPI.Models
{
    [MessageContract]
    public class SendEmailResponse
    {
        [MessageBodyMember]
        public string Response { get; set; }
    }
}