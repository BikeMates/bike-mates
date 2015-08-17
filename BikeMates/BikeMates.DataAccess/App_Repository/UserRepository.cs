using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeMates.Contracts;
using BikeMates.DataAccess.Entity;

namespace BikeMates.DataAccess.App_Repository
{
    class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(ApplicationUser entity)
        {
            //this.context.Set<ApplicationUser>().Add(entity);
            //SaveChanges();
        }

        public void Delete(ApplicationUser entity)
        {
            this.context.Set<ApplicationUser>().Remove(entity);
            SaveChanges();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return this.context.Users.ToList();
        }

        public ApplicationUser Get(string id)
        {
           return this.context.Set<ApplicationUser>().Find(id);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
