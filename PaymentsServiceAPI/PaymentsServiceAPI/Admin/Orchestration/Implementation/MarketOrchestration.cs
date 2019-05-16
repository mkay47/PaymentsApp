using CentralService.Admin.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CentralService.Admin.Orchestration
{
    public class MarketOrchestration : IMarketOrchestration
    {
        private readonly IConfiguration _Config;
        private readonly string ConnectionString;

        public MarketOrchestration(IConfiguration config)
        {
            ConnectionString = _Config.GetConnectionString("PaymentsServiceDBConnectionString");
            _Config = config;
        }

        public User GetUser(Buy buy)
        {
            string sql = "SELECT * FROM ServiceUser WHERE AccountNumber = @Account;";

            using (var connection = new SqlConnection(ConnectionString))
            {
                var result = connection.QuerySingle<User>(sql, new { buy.Account });

                return result;
            }
        }

        public int LoadAccount(Account account)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Account", account.AccountInfo);
                parameter.Add("@Amount", account.Amount);

                var res = connection.Execute("MakePayment", parameter, commandType: CommandType.StoredProcedure);

                return res;
            }
        }

        public int MakePayment(Buy buy)
        {
            using (var connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PaymentsServiceDB;Integrated Security=True"))
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Account", buy.Account);
                parameter.Add("@Amount", buy.Amount);
                parameter.Add("@TransactionDescription", buy.Description);
                parameter.Add("@Reference", buy.Reference);
                parameter.Add("@Merchant", buy.Merchant);

                var res = connection.Execute("MakePayment", parameter, commandType: CommandType.StoredProcedure);

                return res;
            }
        }
    }
}