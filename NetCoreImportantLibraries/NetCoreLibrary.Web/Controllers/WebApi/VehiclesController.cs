using Microsoft.AspNetCore.Mvc;

namespace NetCoreLibrary.Web.Controllers.WebApi
{
    public class VehiclesController : BaseApiController
    {
        /// <summary>
        /// Rate limit kontrolü için
        /// </summary>
        [HttpGet]
        public IActionResult RateLimitControl()
        {
            return Ok("No Problem!");
        }

        [HttpPut]
        public IActionResult DeleteVehicle(int id)
        {
            return NoContent();
        }
    }
}
