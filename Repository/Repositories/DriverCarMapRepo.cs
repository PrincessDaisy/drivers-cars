using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Data;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class DriverCarMapRepo(DataContext dataContext, ILogger<DriverCarMapRepo> logger)
    {
        private readonly DataContext _dbContext = dataContext;
        private readonly ILogger<DriverCarMapRepo> _logger = logger;

        public async Task<DriverCarMap> Create(DriverCarMap model)
        {
            try
            {
                var driver = await _dbContext.DriverCarMaps.AddAsync(model);

                await _dbContext.SaveChangesAsync();

                return driver.Entity;
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
                return await _dbContext.DriverCarMaps.ToListAsync();
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

        public async Task<bool> Update(DriverCarMap model)
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

        public async Task Delete(DriverCarMap model)
        {
            try
            {
                _dbContext.DriverCarMaps.Remove(model);

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
