using System.Runtime.Serialization;

namespace NotificationServiceAPI.Models
{
    [DataContract]
    public class Email
    {
        [DataMember]
        public string Destination { get; set; }

        [DataMember]
        public string EmailMessage { get; set; }
    }
}