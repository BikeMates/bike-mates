﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using BikeMates.Domain.Entities;
using System.Collections;

namespace BikeMates.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User, string>
    {
        IdentityResult Register(User user, string password); 
        User Login(string email, string password);
        User GetUserByEmail(string email);
        string ForgotPassword(string id);
        IdentityResult ResetPassword(string id, string code, string password);
        IdentityResult ChangePassword(string oldPass, string newPass, string id);
        //void SubscribeRoute(Route route, User user);		
        //bool UnsubscribeRoute(Route route, User user);		
    }
}
