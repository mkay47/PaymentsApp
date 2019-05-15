using CentralService.Admin.Models;
using CentralService.Helper;
using Dapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CentralService.Admin.Orchestration
{
    public class UserOrchestration : IUserOrchestration
    {
        private readonly AppSettings _appSettings;

        public UserOrchestration(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public User GetUser(User user)
        {
            string sql = "SELECT * FROM ServiceUser WHERE (Username = @Username) and (Password = @Password) ;";

            using (var connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PaymentsServiceDB;Integrated Security=True"))
            {
                var result = connection.QuerySingle<User>(sql, new { user.Username, user.Password });

                // remove password before returning
                result.Password = null;

                if (user == null)
                {
                    return null;
                }

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Username.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                user.Token = tokenHandler.WriteToken(token);

                return result;
            }
        }

        public User GetAccount(Sell sell)
        {
            string sql = "SELECT * FROM ServiceUser WHERE AccountNumber = @Account;";

            using (var connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PaymentsServiceDB;Integrated Security=True"))
            {
                var result = connection.QuerySingle<User>(sql, new { sell.Account });

                return result;
            }
        }

        public User GetMerchant(string merchant)
        {
            string sql = "SELECT * FROM ServiceUser WHERE AccountNumber = @Account;";

            using (var connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PaymentsServiceDB;Integrated Security=True"))
            {
                var result = connection.QuerySingle<User>(sql, new { Account = merchant });

                return result;
            }
        }
    }
}