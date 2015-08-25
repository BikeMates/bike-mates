using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using BikeMates.Domain.Entities;

namespace BikeMates.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User, string>
    {
        User Get(string id);
        IdentityResult Register(User user, string password); 
        User Login(string email, string password);
    }
}
