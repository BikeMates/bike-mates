﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

//wsmu -why so many using
using BikeMates.Application.Services;
using BikeMates.Domain.Entities;
using BikeMates.Contracts.Services;
using BikeMates.DataAccess.Repository;
using BikeMates.DataAccess;
using BikeMates.Service.Models;

//for identifiying user
using Microsoft.AspNet.Identity;
using System.Security.Claims;

using System.Web.Security;
using System.Web;

namespace BikeMates.Service.Controllers
{
    public class ProfileController : ApiController
    {   
        private UserService userService;

        public ProfileController()
        {
            userService = new UserService(new UserRepository(new BikeMatesDbContext()));
        }
        
        // GET api/user
        public User Get()
        {
           
           // ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            //string userId = principal.Claims.Where(c => c.Type == "id").Single().Value;
            return userService.GetUser("749eae97-ff20-4d8c-8bd0-7e7fc27a9ed2");//"749eae97-ff20-4d8c-8bd0-7e7fc27a9ed2"
         //  return new User() { FirstName = "Vasya", About = "I like cycling", Id = "vasua123", Email = "vasya@google.com", SecondName = "Vasyonov", Picture = "C:/Users/user/Source/Repos/bike-mates/BikeMates/BikeMates.Service/App_Data/749eae97-ff20-4d8c-8bd0-7e7fc27a9ed2.jpeg" };
        }

        // GET api/user/1
        public User Get(string id)
        {
           return userService.GetUser(id);
        }

        // POST api/user
        public void Update( EditProfileViewModel user)
        {
            User _user = userService.GetUser(user.Id);
            
            
            _user.FirstName = user.FirstName;
            _user.About = user.About;
            _user.SecondName = user.SecondName;
            _user.Picture = user.Picture;
            
           userService.Update(_user);
        }




    }
}
