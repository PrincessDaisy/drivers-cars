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
                _dbContext.Cars.Remove(model);

                await _dbContext.SaveChangesAsync();

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
