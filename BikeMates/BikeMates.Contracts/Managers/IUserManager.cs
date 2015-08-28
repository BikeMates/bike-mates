using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Contracts.Managers
{
    public interface IUserManager
    {
        IdentityResult Create(User user, string password);
        IdentityResult AddToRole(string userId, string role);
        IdentityResult ChangePassword(string userId, string oldPassword, string newPassword);
        User Find(string email, string password);
        User FindByEmail(string email);
    }
}
