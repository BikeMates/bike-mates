using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Contracts.Services
{
    public interface IUserService
    { 
        IdentityResult Register(User user, string password);
        IdentityResult ChangePassword(string oldPassword, string newPassword, string newPasswordConfirmation, string id); 
        User Login(string email, string password);
        void ForgotPassword(string id, string host);
        IdentityResult ResetPassword(string id, string code, string password);
        User GetUserByEmail(string email);
        User GetUser(string id);
        void Delete(string id);
        void Update(User user);
        IEnumerable<User> GetAll();
        void SubscribeRoute(Route route, User user);
        bool UnsubscribeRoute(Route route, User user);

    }
}
