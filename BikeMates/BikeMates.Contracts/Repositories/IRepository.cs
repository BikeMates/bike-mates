using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Contracts.Repositories
{
    public interface IRepository<T, TKey>
    {
        T Add(T entity); //TODO: Make the method void. Do not return added entity. Use Get method to retrieve it.
        void Delete(TKey entity);
        T Get(TKey id);
        IEnumerable<T> GetAll();
        void Update(T entity);
    }
}
