using Microsoft.AspNetCore.Mvc;
using NetCoreLibrary.Core.Domain;

namespace NetCoreLibrary.Web.Controllers.WebApi
{
    [ApiController, Route("api/[controller]/[action]")]
    public class CustomersApiController : ControllerBase
    {
        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            return Ok();
        }
    }
}
