using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CentralService.Admin.Models
{
    public class User
    {
        [JsonProperty("username")]
        [Required(ErrorMessage = "UserName is required")]
        public string Username { get; set; }

        [JsonProperty("password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("balance")]
        public double Balance { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}