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
        IdentityResult Register(User user, string password); 
        User Login(string email, string password);
        User getUserByEmail(string email);
        string forgotPassword(string id);
        IdentityResult resetPassword(string id, string code, string password);
        IdentityResult changePassword(string oldPass, string newPass, string id);
    }
}
