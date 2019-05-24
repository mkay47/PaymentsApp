using CentralService.Admin.Models;

namespace CentralService.Admin.Orchestration
{
    public interface IUserOrchestration
    {
        User GetUser(User user);

        User GetAccount(Sell sell);

        User GetMerchant(string merchant);
    }
}