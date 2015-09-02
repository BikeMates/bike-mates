using BikeMates.Contracts.Repositories;
using BikeMates.Domain.Entities;
using BikeMates.Contracts.Managers;
using BikeMates.DataAccess.Managers;
using Microsoft.AspNet.Identity;

namespace BikeMates.DataAccess.Repository
{
    public class UserRepository :  Repository<User,string>, IUserRepository
    {
        private readonly IUserManager userManager;

        public UserRepository(BikeMatesDbContext context)
            : base(context)
        {
            userManager = new UserManager(context); //TODO: Inject IUserManager via constructor
        }
        
        public IdentityResult Register(User user, string password)
        {
            var result = userManager.Create(user, password);
            if (result.Succeeded)
            {
                result = userManager.AddToRole(user.Id, "user");
            }

            return result;
        }

        public User Login(string email, string password)
        {
            return userManager.Find(email, password);
        }

        public string ForgotPassword(string id)
        {
            return userManager.GeneratePasswordResetToken(id);
        }

        public IdentityResult ChangePassword(string oldPass, string newPass, string id)
        {
            return userManager.ChangePassword(id, oldPass, newPass);
        }

        public User GetUserByEmail(string email)
        {
            return userManager.FindByEmail(email);
        }

        public IdentityResult ResetPassword(string id, string code, string password)
        {
            return userManager.ResetPassword(id, code, password);
        }
    }
}
