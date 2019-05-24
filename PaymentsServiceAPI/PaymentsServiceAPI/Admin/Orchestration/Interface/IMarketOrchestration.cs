using CentralService.Admin.Models;

namespace CentralService.Admin.Orchestration
{
    public interface IMarketOrchestration
    {
        User GetUser(Buy buy);

        int MakePayment(Buy buy);

        int LoadAccount(Account account);
    }
}