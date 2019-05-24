using System.ServiceModel;

namespace NotificationServiceAPI.Models
{
    [MessageContract]
    public class SendSMSResponse
    {
        [MessageBodyMember]
        public string Response { get; set; }
    }
}