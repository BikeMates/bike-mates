using BikeMates.Contracts;
using BikeMates.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Application.Services
{
    public class UserService:IUserService
    {
        private IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }
        public ApplicationUser GetUser(string Id)
        {
            return this.repository.Get(Id);
        }

        public void Delete(string id)
        {
            var user = GetUser(id);
            repository.Delete(user);
        }
    }
}
