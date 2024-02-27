using drivers_cars.DTO;
using Repository.Interfaces;
using Repository.Models;

namespace drivers_cars.Services
{
    public class CarsService(ICarRepo repo)
    {
        private readonly ICarRepo _repo = repo;
        public async Task<CarDTO> Create(CarDTO dto)
        {
            var car = Helpers.Helpers.MapObjects<CarDTO, Car>(dto);

            var result = await _repo.Create(car);

            return Helpers.Helpers.MapObjects<Car, CarDTO>(result);
        }

        public async Task<bool> Update(CarDTO dto)
        {
            var car = Helpers.Helpers.MapObjects<CarDTO, Car>(dto);

            await _repo.Update(car);

            return true;
        }

        public async Task<IEnumerable<CarDTO>> GetAll()
        {
            var result = await _repo.GetAll();

            List<CarDTO> resultDTO = [];

            foreach (var Car in result)
            {
                resultDTO.Add(Helpers.Helpers.MapObjects<Car, CarDTO>(Car));
            }

            return resultDTO;
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _repo.GetById(id) ?? throw new KeyNotFoundException($"Item with ID = {id} not found");
            await _repo.Delete(item);

            return true;
        }

        public async Task<bool> DeleteAll()
        {
            await _repo.DeleteAll();

            return true;
        }
    }
}
