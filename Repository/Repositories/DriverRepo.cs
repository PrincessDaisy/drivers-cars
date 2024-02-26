using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
    public class DriverRepo(DataContext dataContext, ILogger<DriverRepo> logger) : IDriverRepo
    {
        private readonly DataContext _dbContext = dataContext;
        private readonly ILogger<DriverRepo> _logger = logger;

        public async Task<Driver> Create(Driver model)
        {
            try
            {
                var driver = await _dbContext.Drivers.AddAsync(model);

                await _dbContext.SaveChangesAsync();

                return driver.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            
        }

        public async Task<IEnumerable<Driver>> GetAll()
        {
            try
            {
                return await _dbContext.Drivers.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Driver> GetById(int id)
        {
            try
            {
                return await _dbContext.Drivers.SingleOrDefaultAsync(d => d.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> Update(Driver model)
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

        public async Task Delete(Driver model)
        {
            try
            {
                _dbContext.Drivers.Remove(model);

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
