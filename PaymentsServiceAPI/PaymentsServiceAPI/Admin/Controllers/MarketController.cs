using CentralService.Admin.Models;
using CentralService.Admin.Orchestration;
using CentralServiceAPI.web.Notification.Models;
using CentralServiceAPI.web.Notification.Orchestrations;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CentralService.Admin.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class MarketController : Controller
    {
        private readonly IMarketOrchestration _MarketOrhestration;
        private readonly INotificationOrchestration _NotificationOrchestration;
        private readonly IUserOrchestration _UserOrchestration;

        public MarketController(IMarketOrchestration marketOrchestration,
                                INotificationOrchestration notificationOrchestration,
                                IUserOrchestration userOrchestration)
        {
            _MarketOrhestration = marketOrchestration;
            _NotificationOrchestration = notificationOrchestration;
            _UserOrchestration = userOrchestration;
        }

        [Route("buy")]
        [HttpPost()]
        public ActionResult Buy([FromBody] Buy buy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(buy);
            }

            var merchant = _UserOrchestration.GetMerchant(buy.Merchant);

            var user = _MarketOrhestration.GetUser(buy);

            if (buy.Amount > user.Balance)
            {
                return BadRequest("Insufficient Funds, Please load Account");
            }

            var res = _MarketOrhestration.MakePayment(buy);

            var smsMerchant = _NotificationOrchestration.SendSMS(
                new MessageInput { Destination = merchant.Phone, Message = $"{user.Name} bought {buy.Description} for R{buy.Amount}" });

            var emailMerchant = _NotificationOrchestration.SendEmail(
                new MessageInput { Destination = merchant.Email, Message = $"{user.Name} bought {buy.Description} for R{buy.Amount}" });

            var smsUser = _NotificationOrchestration.SendSMS(
                new MessageInput { Destination = user.Phone, Message = $"{merchant.Name} received R{buy.Amount} for R{buy.Description}" });

            var emailUser = _NotificationOrchestration.SendEmail(
                new MessageInput { Destination = merchant.Email, Message = $"{user.Name} bought {buy.Description} for R{buy.Amount}" });

            return Content($"{res}");
        }

        [Route("sell")]
        [HttpPost()]
        public ActionResult Sell([FromBody] Sell sell)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }

            var user = _UserOrchestration.GetAccount(sell);

            var merchant = _UserOrchestration.GetMerchant(sell.Merchant);

            var sms = _NotificationOrchestration.SendSMS(
                new MessageInput { Destination = user.Phone, Message = $"{merchant.Name}" });

            var email = _NotificationOrchestration.SendEmail(
                new MessageInput { Destination = user.Email, Message = $"" });

            return Content("");
        }

        [Route("account")]
        [HttpPost]
        public ActionResult LoadAccount([FromBody] Account account)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Model");
                }
                var r = _MarketOrhestration.LoadAccount(account);

                return Content($"{r}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}