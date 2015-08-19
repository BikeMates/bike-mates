using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Contracts.Services
{
    public interface IUserService
    {
        IdentityResult Register(User entity, string password);
        User GetUser(string id);
        void Delete(string id);
        void Update(User entity);

    }
}
