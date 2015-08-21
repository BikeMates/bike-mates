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
        public void Add(User entity)
        {

        }

        public void Delete(User entity)
        {
            this.context.Set<User>().Remove(entity);
            SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return this.context.Users.ToList();
        }

        public User Get(string id)
        {
           return this.context.Set<User>().Find(id);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public void Update(User entity)
        {
            this.context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            this.context.SaveChanges();
        }

        public IdentityResult Register(User entity, string password)
        {
            var idResult = userManager.Create(entity, password);

            return idResult;
        }

        public User Login(string email, string password)
        {
            User user = userManager.Find(email, password);
            return user;
        }
    }
}
