using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using BikeMates.Domain.Entities;

namespace BikeMates.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User Get(string id);
        IdentityResult Register(User entity, string password); //TODO: Rename entity to user. 
    }
}
