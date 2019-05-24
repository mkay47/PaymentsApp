using System.Runtime.Serialization;

namespace NotificationServiceAPI.Models
{
    [DataContract]
    public class SMS
    {
        [DataMember]
        public string Destination { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}