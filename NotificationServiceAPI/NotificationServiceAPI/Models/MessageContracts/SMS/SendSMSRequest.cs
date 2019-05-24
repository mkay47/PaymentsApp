using System.ServiceModel;

namespace NotificationServiceAPI.Models
{
    [MessageContract]
    public class SendSMSRequest
    {
        [MessageHeader]
        public ServiceToken ServiceToken { get; set; }

        [MessageBodyMember]
        public SMS Message { get; set; }
    }
}