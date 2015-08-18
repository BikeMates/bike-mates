using BikeMates.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Contracts
{
    //TODO: Create folder Repositories and move this interface into it
    //TODO: Create a base IRepository interface
    public interface IUserRepository
    {
        void Add(ApplicationUser entity);
        void Delete(ApplicationUser entity);
        IEnumerable<ApplicationUser> GetAll();
        ApplicationUser Get(string id);
        void SaveChanges(); //TODO: Remove this method. Use it inside each methods like Add, Delete, etc.
    }
}
