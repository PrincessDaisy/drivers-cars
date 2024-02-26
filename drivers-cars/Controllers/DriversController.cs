using drivers_cars.DTO;
using drivers_cars.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<DriverDTO>> GetAll()
        {
            return await _service.GetAll();
        }
    }
}
