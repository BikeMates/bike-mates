using BikeMates.Contracts.Managers;
using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace BikeMates.DataAccess.Managers
{
    public class UserManager:IUserManager
    {
        private readonly UserManager<User> userManager;
        public UserManager(BikeMatesDbContext context) 
        {
            userManager = new UserManager<User>(new UserStore<User>(context));
            var provider = new DpapiDataProtectionProvider("BikeMates");
            userManager.UserTokenProvider = new DataProtectorTokenProvider<User>(provider.Create("ResetPassword"));
        }

        public IdentityResult Create(User user, string password)
        {
            return userManager.Create(user, password); 
        }

        public IdentityResult AddToRole(string userId, string role)
        {
            return userManager.AddToRole(userId, role);
        }

        public IdentityResult ChangePassword(string userId, string oldPassword, string newPassword)
        {
            return userManager.ChangePassword(userId, oldPassword, newPassword);
        }

        public User Find(string email, string password)
        {
            return userManager.Find(email, password); 
        }

        public User FindByEmail(string email)
        {
            return userManager.FindByEmail(email);
        }


        public string GeneratePasswordResetToken(string userId)
        {
            return userManager.GeneratePasswordResetToken(userId);
        }


        public IdentityResult ResetPassword(string userId, string code, string password)
        {
            return userManager.ResetPassword(userId, code, password);
        }
    }
}
