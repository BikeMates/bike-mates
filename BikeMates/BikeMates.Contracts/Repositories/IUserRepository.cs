using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;

namespace BikeMates.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User, string>
    {
        //TODO: Rename id to userId. Be more specific
        IdentityResult Register(User user, string password); 
        User Login(string email, string password);
        User GetUserByEmail(string email);
        string ForgotPassword(string id);
        IdentityResult ResetPassword(string id, string code, string password);
        IdentityResult ChangePassword(string oldPassword, string newPassword, string id);
    }
}
