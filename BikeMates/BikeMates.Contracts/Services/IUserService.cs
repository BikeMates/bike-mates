using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Contracts.Services
{
    public interface IUserService
    { 
        IdentityResult Register(User user, string password);
        IdentityResult ChangePassword(string oldPass, string newPass, string newPassConfirmation, string id); //TODO: Rename Pass to Password
        User Login(string email, string password);
        void forgotPassword(string id, string host);
        IdentityResult resetPassword(string id, string code, string password);
        User getUserByEmail(string email);
        User GetUser(string id);
        void Delete(string id);
        void Update(User user);
        IEnumerable<User> GetAll();

    }
}
