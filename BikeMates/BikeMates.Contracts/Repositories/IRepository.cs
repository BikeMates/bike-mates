using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Contracts.Repositories
{
    public interface IRepository<T, TKey>
    {
        void Add(T entity);
        void Delete(TKey entity);
        IEnumerable<T> GetAll();
        void Update(T entity);
    }
}
