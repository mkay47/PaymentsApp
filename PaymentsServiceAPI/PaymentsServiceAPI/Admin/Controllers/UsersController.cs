using CentralService.Admin.Models;
using CentralService.Admin.Orchestration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace CentralService.Admin.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class UsersController : Controller
    {
        private readonly IUserOrchestration _UserOrchestration;

        public UsersController(IUserOrchestration userOrchestration)
        {
            _UserOrchestration = userOrchestration;
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost()]
        public ActionResult<User> Authenticate([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var result = _UserOrchestration.GetUser(user);

                var response = JsonConvert.SerializeObject(result);

                return Content(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}