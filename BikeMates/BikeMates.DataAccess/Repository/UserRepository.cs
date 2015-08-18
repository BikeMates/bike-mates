using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeMates.Contracts;
using BikeMates.DataAccess.Entities;
using BikeMates.Contracts.Repositories;

namespace BikeMates.DataAccess.Repository
{
    class UserRepository : IUserRepository
    {
        private readonly BikeMatesDbContext context;

        public UserRepository(BikeMatesDbContext context)
        {
            this.context = context;
        }
        public void Add(User entity)
        {
            //this.context.Set<ApplicationUser>().Add(entity);
            //SaveChanges();
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
        }
    }
}
