using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Contracts.Repositories
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        T Get(string id);
    }
}
