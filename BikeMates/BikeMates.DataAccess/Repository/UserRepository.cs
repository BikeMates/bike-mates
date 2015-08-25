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

namespace BikeMates.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BikeMatesDbContext context;
        private readonly UserManager<User> userManager;

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
            this.context.Set<User>().Remove(user);
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
            var idResult = userManager.Create(user, password); 

            return idResult;
        }

        public User Login(string email, string password)
        {
            User user = userManager.Find(email, password);
            return user;
        }
    }
}
