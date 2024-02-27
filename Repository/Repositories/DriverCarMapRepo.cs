using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Data;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class DriverCarMapRepo(DataContext dataContext, ILogger<DriverCarMapRepo> logger) : IDriverCarMapRepo
    {
        private readonly DataContext _dbContext = dataContext;
        private readonly ILogger<DriverCarMapRepo> _logger = logger;

        public async Task<DriverCarMap> Create(int driverId, string carRegNum)
        {
            try
            {
                var driver = await _dbContext.Drivers.SingleOrDefaultAsync(x => x.Id == driverId) ??
                             throw new KeyNotFoundException($"Driver with id = {driverId} not found");
                var car = await _dbContext.Cars.SingleOrDefaultAsync(x => x.RegistrationNumber == carRegNum) ??
                          throw new KeyNotFoundException($"Car with registration number = {carRegNum} not found");

                var checkExist = await _dbContext.DriverCarMaps.SingleOrDefaultAsync(x => x.Driver == driver && x.Car == car);

                if (checkExist != null)
                {
                    throw new SqlAlreadyFilledException("This map already exist");
                }

                DriverCarMap entity = new()
                {
                    Car = car,
                    Driver = driver
                };

                var result = await _dbContext.DriverCarMaps.AddAsync(entity);

                await _dbContext.SaveChangesAsync();


                return result.Entity;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        public async Task<IEnumerable<DriverCarMap>> GetAll()
        {
            try
            {
                return await _dbContext.DriverCarMaps
                    .Include(x => x.Driver)
                    .Include(x => x.Car)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<DriverCarMap> GetById(int id)
        {
            try
            {
                return await _dbContext.DriverCarMaps.SingleOrDefaultAsync(d => d.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task Delete(int driverId, string carRegNum)
        {
            try
            {
                var driver = await _dbContext.Drivers.SingleOrDefaultAsync(x => x.Id == driverId) ?? 
                             throw new KeyNotFoundException($"Driver with id = {driverId} not found");

                var car = await _dbContext.Cars.SingleOrDefaultAsync(x => x.RegistrationNumber == carRegNum) ?? 
                          throw new KeyNotFoundException($"Car with registration number = {carRegNum} not found");

                var result = await _dbContext.DriverCarMaps.SingleOrDefaultAsync(d => d.Driver == driver && d.Car == car) ?? 
                             throw new KeyNotFoundException($"Car with registration number {carRegNum} not mapped with driver id {driverId}");

                _dbContext.DriverCarMaps.Remove(result);

                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
