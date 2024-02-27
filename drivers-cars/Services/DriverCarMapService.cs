using drivers_cars.DTO;
using drivers_cars.Models;
using Repository.Interfaces;
using Repository.Models;

namespace drivers_cars.Services
{
    public class DriverCarMapsService(IDriverCarMapRepo repo)
    {
        private readonly IDriverCarMapRepo _repo = repo;
        public async Task<DriverCarMapDTO> Create(MappingRequest request)
        {
            
            var result = await _repo.Create(request.DriverId, request.CarRegNumber);

            var driverDto = Helpers.Helpers.MapObjects<Driver, DriverDTO>(result.Driver);

            var carDto = Helpers.Helpers.MapObjects<Car, CarDTO>(result.Car);

            return new DriverCarMapDTO()
            {
                Id = result.Id,
                Driver = driverDto,
                Car = carDto
            };
        }

        //public async Task<bool> Update(DriverCarMapDTO dto)
        //{
        //    var driverCarMap = Helpers.Helpers.MapObjects<DriverCarMapDTO, DriverCarMap>(dto);

        //    await _repo.Update(driverCarMap);

        //    return true;
        //}

        public async Task<IEnumerable<DriversWithCarsDTO>> GetAll()
        {
            var result = await _repo.GetAll();

            if (result is null)
            {
                return new List<DriversWithCarsDTO>();
            }

            var drivers = result.Select(x => x.Driver).Distinct();

            List<DriversWithCarsDTO> driversWithCars = new();

            foreach (var item in drivers)
            {
                int? age = null;
                if (item.BirthDate is not null)
                {
                    age = Helpers.Helpers.GetAge((DateOnly)item.BirthDate);
                }

                DriversWithCarsDTO mapping = new()
                {
                    FIO = $"{item.FirstName} {item.Surname} {item.MiddleName}",
                    Age = age,
                    Cars = new List<MappedCar>()
                };

                var cars = result.Where(x => x.Driver.Id == item.Id).Select(x => new MappedCar()
                {
                    Model = $"{x.Car.Brand} ({x.Car.Model})",
                    RegNum = x.Car.RegistrationNumber
                }).ToList();

                mapping.Cars = cars;

                driversWithCars.Add(mapping);
            }

            return driversWithCars;
        }

        public async Task<bool> Delete(MappingRequest request)
        {
            
            await _repo.Delete(request.DriverId, request.CarRegNumber);

            return true;
        }
    }
}
