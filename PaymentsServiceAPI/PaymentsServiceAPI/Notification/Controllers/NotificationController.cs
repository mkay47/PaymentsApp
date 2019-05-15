using CentralServiceAPI.web.Notification.Models;
using CentralServiceAPI.web.Notification.Orchestrations;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CentralServiceAPI.web.Notification
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class NotificationController : Controller
    {
        private readonly INotificationOrchestration _NotificationOrchestration;

        public NotificationController(INotificationOrchestration notificationOrchestration)
        {
            _NotificationOrchestration = notificationOrchestration;
        }

        [Route("email")]
        [HttpPost()]
        public ActionResult SendEmail([FromBody] MessageInput messageInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var reply = _NotificationOrchestration.SendEmail(messageInput);

                    if (reply != null)
                    {
                        return Content(reply.Response);
                    }

                    return BadRequest();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("sms")]
        [HttpPost()]
        public ActionResult SendSMS([FromBody] MessageInput messageInput)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var reply = _NotificationOrchestration.SendSMS(messageInput);

                    if (reply != null)
                    {
                        return Content(reply.Response);
                    }

                    return BadRequest();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}