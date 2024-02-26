using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IBaseRepo<T>
    {
        public Task<T> Create(T _object);

        public Task<bool> Update(T _object);

        public Task<IEnumerable<T>> GetAll();

        public Task<T> GetById(int Id);

        public Task Delete(T _object);
    }
}
