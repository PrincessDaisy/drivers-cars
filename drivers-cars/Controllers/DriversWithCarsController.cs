using System.Data.SqlTypes;
using Azure.Core;
using drivers_cars.Models;
using drivers_cars.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace drivers_cars.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriversWithCarsController(ILogger<DriversWithCarsController> logger, DriverCarMapsService service) : ControllerBase
    {
        private readonly ILogger<DriversWithCarsController> _logger = logger;
        private readonly DriverCarMapsService _service = service;

        [HttpGet("get-driver-with-cars")]
        public async Task<ActionResult> GetDriversWithCars()
        {
            try
            {
                return Ok(await _service.GetAll());
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
            
        }

        [HttpPost("map-car-to-driver")]
        public async Task<ActionResult> MapCarToDriver(MappingRequest request)
        {
            try
            {
                var result = await _service.Create(request);

                return Ok(result);
            }
            catch (SqlAlreadyFilledException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteMap(MappingRequest request)
        {
            try
            {
                var result = await _service.Delete(request);

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
