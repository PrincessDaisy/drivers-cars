using drivers_cars.DTO;
using drivers_cars.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace drivers_cars.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController(ILogger<CarsController> logger, CarsService service) : ControllerBase
    {
        private readonly ILogger<CarsController> _logger = logger;
        private readonly CarsService _service = service;

        [HttpGet]
        public async Task<IEnumerable<CarDTO>> GetAll()
        {
            return await _service.GetAll();
        }

        /// <summary>
        /// Create car record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create([FromBody] CarDTO request)
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
        /// Update car record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update([FromBody] CarDTO request)
        {
            try
            {
                return Ok(await _service.Update(request));
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Delete car record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Delete all car records
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete-all")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete()
        {
            try
            {
                await _service.DeleteAll();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}