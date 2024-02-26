using Repository.Models;

namespace drivers_cars.DTO
{
    public class DriverCarMapDTO
    {
        public int Id { get; set; }

        public DriverDTO Driver { get; set; }

        public CarDTO Car { get; set; }
    }
}
