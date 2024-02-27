using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Data;
using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CarRepo(DataContext dataContext, ILogger<CarRepo> logger) : ICarRepo
    {
        private readonly DataContext _dbContext = dataContext;
        private readonly ILogger<CarRepo> _logger = logger;

        public async Task<Car> Create(Car model)
        {
            try
            {
                var driver = await _dbContext.Cars.AddAsync(model);

                await _dbContext.SaveChangesAsync();

                return driver.Entity;
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

        public async Task<IEnumerable<Car>> GetAll()
        {
            try
            {
                return await _dbContext.Cars.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Car> GetById(int id)
        {
            try
            {
                return await _dbContext.Cars.SingleOrDefaultAsync(d => d.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> Update(Car model)
        {
            try
            {
                _dbContext.Entry(model).State = EntityState.Modified;

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task Delete(Car model)
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync();
                var map = await _dbContext.DriverCarMaps.Where(d => d.Id == model.Id).ToListAsync();

                foreach (var item in map)
                {
                    _dbContext.DriverCarMaps.Remove(item);
                }

                _dbContext.Cars.Remove(model);

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task DeleteAll()
        {
            try
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync();
                foreach (var item in _dbContext.Cars)
                {
                    _dbContext.Cars.Remove(item);
                }
                foreach (var item in _dbContext.DriverCarMaps)
                {
                    _dbContext.DriverCarMaps.Remove(item);
                }
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
