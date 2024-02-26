using drivers_cars.DTO;
using Repository.Interfaces;
using Repository.Models;

namespace drivers_cars.Services
{
    public class DriverCarMapsService(IDriverCarMapRepo repo)
    {
        private readonly IDriverCarMapRepo _repo = repo;
        public async Task<DriverCarMapDTO> Create(DriverCarMapDTO dto)
        {
            var driverCarMap = Helpers.Helpers.MapObjects<DriverCarMapDTO, DriverCarMap>(dto);
            
            var result = await _repo.Create(driverCarMap);
            
            return Helpers.Helpers.MapObjects<DriverCarMap, DriverCarMapDTO>(result);
        }

        public async Task<bool> Update(DriverCarMapDTO dto)
        {
            var driverCarMap = Helpers.Helpers.MapObjects<DriverCarMapDTO, DriverCarMap>(dto);

            await _repo.Update(driverCarMap);

            return true;
        }

        public async Task<IEnumerable<DriverCarMapDTO>> GetAll()
        {
            var result = await _repo.GetAll();

            List<DriverCarMapDTO> resultDTO = [];

            foreach(var DriverCarMap in result) 
            {
                resultDTO.Add(Helpers.Helpers.MapObjects<DriverCarMap, DriverCarMapDTO>(DriverCarMap));
            }

            return resultDTO;
        }

        public async Task<bool> Delete(int id)
        {
            var item = await _repo.GetById(id) ?? throw new KeyNotFoundException($"Item with ID = {id} not found");
            await _repo.Delete(item);

            return true;
        }
    }
}
