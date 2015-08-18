using BikeMates.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User Get(string id);
        //bool Register(User entity, string password);
    }
}
