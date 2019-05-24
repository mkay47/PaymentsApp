using Newtonsoft.Json;

namespace TestPaymentAPI.Models
{
    public class Nonce
    {
        [JsonProperty("nonce")]
        public string nonce { get; set; }

        [JsonProperty("chargeAmount")]
        public decimal chargeAmount { get; set; }

        public Nonce(string nonce)
        {
            this.nonce = nonce;
            this.chargeAmount = chargeAmount;
        }
    }
}