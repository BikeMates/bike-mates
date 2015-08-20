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

namespace BikeMates.Service.Controllers
{
    public class ProfileController : ApiController
    {
        private IUserService userService = new UserService(new UserRepository(new BikeMatesDbContext()));


        // GET api/user
        public User Get()
        {
            return new User() { FirstName = "Vasya", About = "I like cuclimg", Id = "vasua123", Email = "vasya@google.com", SecondName = "Vasyonov", Picture = "http://localhost:51949/Content/Images/avatar-big.png" };
        }

        // GET api/user/1
        public User Get(string id)
        {
           return userService.GetUser(id);
        }

        // POST api/user
        public void Update(User user)
        {
           userService.Update(user);
        }




    }
}
