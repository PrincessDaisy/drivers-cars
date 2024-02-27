using System.Data.SqlTypes;
using System.Net.Mime;
using Azure.Core;
using drivers_cars.Models;
using drivers_cars.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace drivers_cars.Controllers
{
    [ApiController]
    [Route("driver-with-cars")]
    public class DriversWithCarsController(ILogger<DriversWithCarsController> logger, DriverCarMapsService service) : ControllerBase
    {
        private readonly ILogger<DriversWithCarsController> _logger = logger;
        private readonly DriverCarMapsService _service = service;

        [HttpGet("get-drivers-with-cars")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Получение водителей с привязанными авто",
            Description = "Метод обеспечивающий получение записи из таблице DriverCarMaps",
            OperationId = "drivers-with-cars/get-driverd-with-cars"
        )]
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
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Привязка водителя к авто",
            Description = "Метод обеспечивающий создание записи в таблице DriverCarMaps",
            OperationId = "drivers-with-cars/map-car-to-driver"
        )]
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
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Отвязка водителя от авто",
            Description = "Метод обеспечивающий удаление записи в таблице DriverCarMaps",
            OperationId = "drivers-with-cars/delete-map"
        )]
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
