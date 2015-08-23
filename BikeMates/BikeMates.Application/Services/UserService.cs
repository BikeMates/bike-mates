using BikeMates.Contracts;
using BikeMates.Contracts.Repositories;
using BikeMates.Contracts.Services;
using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Application.Services
{
    public class UserService: IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository) 
        {
            this.userRepository = userRepository;
        }
        public User GetUser(string id) 
        {
            return this.userRepository.Get(id);
        }

        public void Delete(string id)
        {
            var user = GetUser(id); //TODO: Move this logic into repository. You should not load user from database for delete operation
            userRepository.Delete(user);
        }

        public void Update(User entity)
        {
            this.userRepository.Update(entity);
         }

        public IdentityResult Register(User entity, string password)
        {
            return this.userRepository.Register(entity, password);
        }


        public User Login(string email, string password)
        {
            return this.userRepository.Login(email, password);
        }
    }
}
