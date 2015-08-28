using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeMates.Contracts;
using BikeMates.Contracts.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BikeMates.Domain.Entities;
using System.Security.Policy;

namespace BikeMates.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BikeMatesDbContext context;
        private readonly UserManager<User> userManager; //TODO: Create IUserManager interface and use it here

        public UserRepository(BikeMatesDbContext context)
        {
            this.context = context;
            userManager = new UserManager<User>(new UserStore<User>(context));
        }
        public void Add(User user)
        {

        }

        public void Delete(string id)
        {
            var user = Get(id);
            this.context.Set<User>().Remove(user); //TODO: Create property Users for context.Users and use it in this class
            context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return this.context.Users.ToList();
        }

        public User Get(string id)
        {
           return this.context.Set<User>().Find(id);
        }

        

        public void Update(User user)
        {
            this.context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            this.context.SaveChanges();
        }

        public IdentityResult Register(User user, string password)
        {
            var result = userManager.Create(user, password); 
            if(result.Succeeded)
            {
                userManager.AddToRole(user.Id, "user");
            }
            return result;
        }

        public User Login(string email, string password)
        {
            return userManager.Find(email, password); 
        }
        
        public void resetPassword(string id)
        {

        }

        public IdentityResult changePassword(string oldPass, string newPass, string id)
        {
            return userManager.ChangePassword(id, oldPass, newPass);
        }
        
        public User getUserByEmail(string email)
        {
            return userManager.FindByEmail(email);
        }
    }
}
