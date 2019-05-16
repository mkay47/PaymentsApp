using CentralService.Admin.Models;
using CentralService.Helper;
using Dapper;
using Microsoft.Extensions.Configuration;
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
        private readonly JWTSettings _JWTSettings;
        private readonly IConfiguration _Config;
        private readonly string ConnectionString;

        public UserOrchestration(IOptions<JWTSettings> jwtSettings,IConfiguration config)
        {
            _JWTSettings = jwtSettings.Value;
            _Config = config;
            ConnectionString = _Config.GetSection("PaymentsServiceDBConnectionString").Value;
        }

        public User GetUser(User user)
        {
            try
            {
                string sql = "SELECT * FROM ServiceUser WHERE (Username = @Username) and (Password = @Password) ;";

                using (var connection = new SqlConnection(ConnectionString))
                {
                    var result = connection.QueryFirstOrDefault<User>(sql, new { user.Username, user.Password });

                    // remove password before returning
                    result.Password = null;

                    if (user == null)
                    {
                        return null;
                    }

                    // authentication successful so generate jwt token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_JWTSettings.SecretKey);
                    var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(
                            new Claim[]
                            {
                            new Claim(ClaimTypes.Name, user.Username.ToString())
                            }
                        ),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = signinCredentials
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    result.Token = tokenHandler.WriteToken(token);

                    return result;
                }
            
            }catch(Exception ex)
            {
                return null;
            }
        }

        public User GetAccount(Sell sell)
        {
            string sql = "SELECT * FROM ServiceUser WHERE AccountNumber = @Account;";

            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.QuerySingle<User>(sql, new { sell.Account });

                return result;
            }
        }

        public User GetMerchant(string merchant)
        {
            string sql = "SELECT * FROM ServiceUser WHERE AccountNumber = @Account;";

            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.QuerySingle<User>(sql, new { Account = merchant });

                return result;
            }
        }
    }
}