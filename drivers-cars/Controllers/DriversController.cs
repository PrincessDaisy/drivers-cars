using drivers_cars.DTO;
using drivers_cars.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace drivers_cars.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriversController : ControllerBase
    {
        private readonly ILogger<DriversController> _logger;
        private readonly DriversService _service;

        public DriversController(ILogger<DriversController> logger, DriversService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Return all driver records
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<DriverDTO>> GetAll()
        {
            try
            {
                return await _service.GetAll();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Create driver record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        
        public async Task<ActionResult> Create([FromBody] DriverDTO request)
        {
            try
            {
                return Ok(await _service.Create(request));
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
            
        }

        /// <summary>
        /// Delete driver record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task Delete(int id)
        {
            await _service.Delete(id);
        }

    }
}
