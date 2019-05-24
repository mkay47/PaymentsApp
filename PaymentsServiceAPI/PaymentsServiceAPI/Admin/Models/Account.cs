using Newtonsoft.Json;

namespace CentralService.Admin.Models
{
    public class Account
    {
        [JsonProperty("account")]
        public string AccountInfo { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }
    }
}