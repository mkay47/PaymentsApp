using System.Runtime.Serialization;

namespace NotificationServiceAPI.Models.DataContracts
{
    [DataContract]
    public class AuthenticationHeader
    {
        [DataMember]
        public string EncryptedSignature { get; set; }
    }
}