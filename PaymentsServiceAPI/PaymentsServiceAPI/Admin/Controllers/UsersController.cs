using CentralService.Admin.Models;
using CentralService.Admin.Orchestration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace CentralService.Admin.Controllers
{
    [Authorize,AllowAnonymous]
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
                    return BadRequest("Invalid Client Requests");
                }

                if(user == null)
                {
                    return BadRequest("Invalid CLient Request");
                }

                var result = _UserOrchestration.GetUser(user);

                if (result == null)
                {
                    return Unauthorized();
                }

                var response = JsonConvert.SerializeObject(result);

                return Content(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("authenticate")]
        [HttpGet]
        public ActionResult Get(int id)
        {
            var message = "Hello Authorized API with ID = " + id;
            return Ok(message);
        }

        // PUT api/WebApi/5  
        [Route("authenticate")]
        [HttpPut]
        public ActionResult Put(int id, [FromBody]User user)
        {
            return NoContent();
        }

        // DELETE api/WebApi/5  
        [Route("authenticate")]
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            return Ok("Session Expired");
        }
    }
}