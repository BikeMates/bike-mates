﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeMates.Contracts;
using BikeMates.Contracts.Repositories;
using BikeMates.Domain.Entities;
using System.Security.Policy;
using BikeMates.Contracts.Managers;
using BikeMates.DataAccess.Managers;
using Microsoft.AspNet.Identity;
using System.Collections;

namespace BikeMates.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BikeMatesDbContext context;
        private readonly IUserManager userManager;

        public UserRepository(BikeMatesDbContext context)
        {
            this.context = context;
            userManager = new UserManager(context); //TODO: Inject IUserManager via constructor
        }
        public User Add(User user)
        {
            return new User(); //FOR_GUYS: implement or delete
        }

        public void Delete(string id)
        {
            var user = Get(id);
            this.context.Set<User>().Remove(user); //TODO: Create property Users for context.Users and use it in this class
            context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return this.context.Users;
        }

        public User Get(string id)
        {
            return this.context.Set<User>().Find(id);
        }



        public void Update(User user)
        {
            this.context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            this.context.SaveChanges();
        }

        public IdentityResult Register(User user, string password)
        {
            var result = userManager.Create(user, password);
            if (result.Succeeded)
            {
                result = userManager.AddToRole(user.Id, "user");
            }
            return result;
        }

        public User Login(string email, string password)
        {
            return userManager.Find(email, password);
        }

        public string forgotPassword(string id)
        {
            return userManager.GeneratePasswordResetToken(id);
        }

        public IdentityResult ChangePassword(string oldPass, string newPass, string id)
        {
            return userManager.ChangePassword(id, oldPass, newPass);
        }

        public User getUserByEmail(string email)
        {
            return userManager.FindByEmail(email);
        }

        public IdentityResult resetPassword(string id, string code, string password)
        {
            return userManager.ResetPassword(id, code, password);
        }

        public void SubscribeRoute(Route route, User user)
        {
            user.Subscriptions.Add(route);
            this.Update(user);
        }
        public bool UnsubscribeRoute(Route route, User user)
        {
            bool isRemoved = user.Subscriptions.Remove(route);
            this.Update(user);
            return isRemoved;
        }

    }
}
