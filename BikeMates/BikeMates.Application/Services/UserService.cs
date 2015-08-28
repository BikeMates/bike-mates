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
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace BikeMates.Application.Services
{
    public class UserService : IUserService
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
            userRepository.Delete(id);
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

        public User getUserByEmail(string email)
        {
            return this.userRepository.getUserByEmail(email);
        }
        public void resetPassword(string id)
        {

        }

        public IdentityResult changePassword(string oldPassword, string newPassword, string newPassConfirmation, string id)
        {
            if (!(String.IsNullOrEmpty(oldPassword)) && !(String.IsNullOrEmpty(newPassword)) && !(String.IsNullOrEmpty(newPassConfirmation))
                && newPassword == newPassConfirmation)
            {
                return this.userRepository.changePassword(oldPassword, newPassword, id);
            }
            //Cause all fields are empty means user do not want to change password
            return IdentityResult.Success; 
        }
    }
}
