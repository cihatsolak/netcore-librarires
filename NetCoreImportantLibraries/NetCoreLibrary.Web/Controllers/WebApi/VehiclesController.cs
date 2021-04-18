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
        public IActionResult DeleteVehicle()
        {
            return NoContent();
        }

        [HttpGet("{id}/{plate}")]
        public IActionResult GetVehicleById(int id, string plate)
        {
            string vehicleDetail = string.Concat(id, " ", plate);

            return Ok(vehicleDetail);
        }
    }
}
