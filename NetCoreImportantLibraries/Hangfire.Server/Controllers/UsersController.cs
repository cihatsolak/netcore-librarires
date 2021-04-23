using Hangfire.Server.Shedules;
using Microsoft.AspNetCore.Mvc;

namespace Hangfire.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public IActionResult SignUp()
        {
            FireAndForgotJobs.EmailSendToUserJob(userId: "123", message: "Hoşgeldin");
            return Ok();
        }
    }
}
