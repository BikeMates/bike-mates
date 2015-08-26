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
        IdentityResult Register(User user, string password);
        User Login(string email, string password);
        void resetPassword(string id);
        User GetUser(string id);
        void Delete(string id);
        void Update(User user);

    }
}
