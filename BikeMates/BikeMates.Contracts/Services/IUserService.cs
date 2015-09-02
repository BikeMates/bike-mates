using System.Collections.Generic;
using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;

namespace BikeMates.Contracts.Services
{
    public interface IUserService
    { 
        IdentityResult Register(User user, string password);
        IdentityResult ChangePassword(string oldPassword, string newPassword, string newPasswordConfirmation, string id); 
        User Login(string email, string password);
        void ForgotPassword(string id, string host);
        IdentityResult ResetPassword(string id, string code, string password);
        User GetUserByEmail(string email);
        User GetUser(string id);
        void Delete(string id);
        void Update(User user);
        IEnumerable<User> GetAll();
    }
}
