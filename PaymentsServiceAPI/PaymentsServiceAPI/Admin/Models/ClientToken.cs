using Newtonsoft.Json;

namespace TestPaymentAPI.Models
{
    public class ClientToken
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        public ClientToken(string token)
        {
            Token = token;
        }
    }
}