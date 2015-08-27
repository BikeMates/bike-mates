using System;
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

//TODO: remove unused usings
//TODO: format code (spaces/tabs, remove blank lines) use - ctrl k, d

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
        public ProfileViewModel Get()
        {
           ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userId = principal.Claims.Where(c => c.Type == "id").Single().Value;



           User _user = userService.GetUser(userId);//"749eae97-ff20-4d8c-8bd0-7e7fc27a9ed2"
            var profile = new ProfileViewModel();
            profile.about = _user.About;
            profile.firstName = _user.FirstName;
            profile.secondName = _user.SecondName;
            profile.picture = _user.Picture;
            profile.id = _user.Id;

            return profile;   
           // return new User() { FirstName = "Vasya", About = "I like cycling", Id = "vasua123", Email = "vasya@google.com", SecondName = "Vasyonov", Picture = "http://localhost:51949/Content/Images/avatar-big.png" };
        }

        // GET api/user/1
        public User Get(string id)
        {
           return userService.GetUser(id);
        }

        // POST api/user
        public void Update( EditProfileViewModel UserViewModel)
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userId = principal.Claims.Where(c => c.Type == "id").Single().Value;


            User user = userService.GetUser(userId); //TODO: use this.user instead of _user


            user.FirstName = UserViewModel.FirstName;
            user.About = UserViewModel.About;
            user.SecondName = UserViewModel.SecondName;
            user.Picture = UserViewModel.Picture;
            
           userService.Update(user);
        }




    }
}
