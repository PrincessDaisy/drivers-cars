using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IDriverCarMapRepo
    {
        public Task<DriverCarMap> Create(int driverId, string carRegNum);

        public Task<IEnumerable<DriverCarMap>> GetAll();

        public Task<DriverCarMap> GetById(int id);

        public Task Delete(int driverId, string carRegNum);
    }
}
