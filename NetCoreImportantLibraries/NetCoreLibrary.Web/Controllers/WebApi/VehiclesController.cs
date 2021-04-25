using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreLibrary.Web.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace NetCoreLibrary.Web.Controllers.WebApi
{
    public class VehiclesController : BaseApiController
    {
        /// <summary>
        /// Swagger örnek endpoint oluşturmak için
        /// </summary>
        /// <remarks>Daha fazla detay açıklaması</remarks>
        /// <param name="example">Örnek request</param>
        /// <returns></returns>
        /// <response code="200">İşlem başarılı</response>
        /// <response code="400">Bulunamadı</response>
        [HttpGet]
        //[Produces("application/json")] //Dönüş json tipinde.
        //[Consumes("application/json")] //Request kesinlikle json tipinde olmalı.
        [SwaggerResponse(200, "Herhangi bir açıklama", typeof(SwaggerTestResponseModel))]
        public IActionResult SwaggerTest(string example)
        {
            if (string.IsNullOrEmpty(example))
            {
                return Ok("No Problem!");
            }

            return NotFound();
        }

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
