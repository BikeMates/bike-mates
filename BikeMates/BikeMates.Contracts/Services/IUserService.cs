using System.Collections.Generic;
using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;

namespace BikeMates.Contracts.Services
{
    public interface IUserService
    { 
        IdentityResult Register(User user, string password);
        IdentityResult ChangePassword(string oldPassword, string newPassword, string newPasswordConfirmation, string userId); 
        User Login(string email, string password);
        void ForgotPassword(string userId, string host);
        IdentityResult ResetPassword(string userId, string code, string password);
        User GetUserByEmail(string email);
        User GetUser(string userId);
        void Delete(string userId);
        void Update(User user);
        IEnumerable<User> GetAll();
        void UnbanUsers(List<string> userIds);
    }
}
