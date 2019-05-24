using Newtonsoft.Json;

namespace CentralService.Admin.Models
{
    public class Sell
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("merchant")]
        public string Merchant { get; set; }
    }
}