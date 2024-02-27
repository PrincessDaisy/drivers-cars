using drivers_cars.DTO;
using drivers_cars.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace drivers_cars.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class CarsController(ILogger<CarsController> logger, CarsService service) : ControllerBase
    {
        private readonly ILogger<CarsController> _logger = logger;
        private readonly CarsService _service = service;

        /// <summary>
        /// Get all records
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Получение всех авто",
            Description = "Метод обеспечивающий возврат всех записей из таблицы Cars",
            OperationId = "cars/get-all"
        )]
        public async Task<ActionResult<IEnumerable<CarDTO>>> GetAll()
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

        /// <summary>
        /// Create car record
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("create")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status208AlreadyReported)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Создание авто",
            Description = "Метод обеспечивающий созданий записи в таблице Cars",
            OperationId = "cars/create"
        )]
        public async Task<ActionResult> Create([FromBody] CarDTO request)
        {
            try
            {
                return Ok(await _service.Create(request));
            }
            catch (DbUpdateException)
            {
                return StatusCode(208, $"Car with registration number {request.RegistrationNumber} already exist");
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
        [HttpPatch("update")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Обновление авто",
            Description = "Метод обеспечивающий обновление записи в таблице Cars",
            OperationId = "cars/update"
        )]
        public async Task<ActionResult> Update([FromBody] CarDTO request)
        {
            try
            {
                return Ok(await _service.Update(request));
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

        /// <summary>
        /// Delete car record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Удаление авто",
            Description = "Метод обеспечивающий удаление записи в таблице Cars",
            OperationId = "cars/delete"
        )]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                return Ok();
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

        /// <summary>
        /// Delete all car records
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete-all")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Удаление всех авто",
            Description = "Метод обеспечивающий удаление всех записей в таблице Cars",
            OperationId = "cars/delete-all"
        )]
        public async Task<ActionResult> DeleteAll()
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