using System.ServiceModel;

namespace NotificationServiceAPI.Models
{
    [MessageContract]
    public class SendEmailRequest
    {
        [MessageHeader]
        public ServiceToken ServiceToken { get; set; }

        [MessageBodyMember]
        public Email Message { get; set; }
    }
}