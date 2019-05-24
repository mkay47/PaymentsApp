using System.Runtime.Serialization;

namespace NotificationServiceAPI.Models
{
    [DataContract]
    public class ServiceToken
    {
        [DataMember]
        public string Token { get; set; }
    }
}