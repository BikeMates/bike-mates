using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace BikeMates.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User, string>
    {
        IdentityResult Register(User user, string password); 
        User Login(string email, string password);
        User GetUserByEmail(string email);
        string ForgotPassword(string userId);
        IdentityResult ResetPassword(string userId, string code, string password);
        IdentityResult ChangePassword(string oldPassword, string newPassword, string userId);
        void BanUser(string userId);
        void UnbanUsers(List<string> userIds);
    }
}
