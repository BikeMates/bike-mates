using BikeMates.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Contracts
{
    public interface IUserRepository
    {
        void Add(ApplicationUser entity);
        void Delete(ApplicationUser entity);
        IEnumerable<ApplicationUser> GetAll();
        ApplicationUser Get(string id);
        void SaveChanges();
    }
}
