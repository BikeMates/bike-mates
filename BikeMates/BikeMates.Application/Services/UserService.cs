using BikeMates.Contracts;
using BikeMates.Contracts.Repositories;
using BikeMates.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Application.Services
{
    public class UserService:IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository repository)
        {
            this.userRepository = repository;
        }
        public User GetUser(string Id)
        {
            return this.userRepository.Get(Id);
        }

        public void Delete(string id)
        {
            var user = GetUser(id);
            userRepository.Delete(user);
        }
    }
}
