﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeMates.Contracts.Managers;
using Microsoft.AspNet.Identity;
using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BikeMates.DataAccess.Managers
{
    class UserManager:IUserManager
    {
        private readonly UserManager<User> userManager;

        public UserManager(BikeMatesDbContext context) 
        {
            userManager = new UserManager<User>(new UserStore<User>(context));
        }

        public IdentityResult Create(User user, string password)
        {
            return userManager.Create(user, password); 
        }

        public IdentityResult AddToRole(string userId, string role)
        {
            return userManager.AddToRole(userId, "user");
        }

        public IdentityResult ChangePassword(string userId, string oldPassword, string newPassword)
        {
            return userManager.ChangePassword(userId, oldPassword, newPassword);
        }

        public User Find(string email, string password)
        {
            return userManager.Find(email, password); 
        }

        public User FindByEmail(string email)
        {
            return userManager.FindByEmail(email);
        }
    }
}
