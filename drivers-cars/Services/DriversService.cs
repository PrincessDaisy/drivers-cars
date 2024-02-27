using drivers_cars.DTO;
using Repository.Interfaces;
using Repository.Models;

namespace drivers_cars.Services
{
    public class DriversService(IDriverRepo repo)
    {
        private readonly IDriverRepo _repo = repo;
        public async Task<DriverDTO> Create(DriverDTO dto)
        {
            var driver = Helpers.Helpers.MapObjects<DriverDTO, Driver>(dto);
            
            var result = await _repo.Create(driver);
            
            return Helpers.Helpers.MapObjects<Driver, DriverDTO>(result);
        }

        public async Task<bool> Update(DriverDTO dto)
        {
            var checkExist = await _repo.GetById(dto.Id) ?? throw new KeyNotFoundException($"Driver not found");

            var driver = Helpers.Helpers.MapObjects<DriverDTO, Driver>(dto);

            await _repo.Update(driver);

            return true;
        }

        public async Task<IEnumerable<DriverDTO>> GetAll()
        {
            var result = await _repo.GetAll();

            List<DriverDTO> resultDTO = [];

            foreach(var driver in result) 
            {
                resultDTO.Add(Helpers.Helpers.MapObjects<Driver, DriverDTO>(driver));
            }

            return resultDTO;
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _repo.GetById(id) ?? throw new KeyNotFoundException($"Driver not found");
            await _repo.Delete(item);

            return true;
        }
    }
}
