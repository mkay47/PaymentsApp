using Newtonsoft.Json;

namespace CentralService.Admin.Models
{
    public class Buy
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("merchant")]
        public string Merchant { get; set; }
    }
}