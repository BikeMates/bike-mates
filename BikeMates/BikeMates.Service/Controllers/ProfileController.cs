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

// why not working ?
//The type 'Microsoft.AspNet.Identity.EntityFramework.IdentityUser' is defined in an assembly that is not referenced. You must add a reference to assembly 'Microsoft.AspNet.Identity.EntityFramework

namespace BikeMates.Service.Controllers
{
    public class ProfileController : ApiController
    {
        private IUserService userService = new UserService(new UserRepository(new BikeMatesDbContext()));

        public User Get(string id)
       {

           return userService.GetUser(id);
        }

        public void Update(User user)
        {
            userService.Update(user);
        }



    }
}
