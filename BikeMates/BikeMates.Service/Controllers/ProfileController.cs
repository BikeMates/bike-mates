﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
// project reffrerences
using BikeMates.Application.Services;
using BikeMates.Domain.Entities;
using BikeMates.DataAccess.Repository;
using BikeMates.DataAccess;
using BikeMates.Service.Models;
//for identifiying user
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

//TODO: format code (spaces/tabs, remove blank lines) use - ctrl k, d

namespace BikeMates.Service.Controllers
{
    [RoutePrefix("api/Profile")]
    public class ProfileController : ApiController
    {
        private UserService userService;

        public ProfileController()
        {
            userService = new UserService(new UserRepository(new BikeMatesDbContext()));
        }

        // GET api/user
        
        public ProfileViewModel Get()
        {   //get logged user id
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userId = principal.Claims.Where(c => c.Type == "id").Single().Value;

            User _user = userService.GetUser(userId); //TODO: Do not use '_' in names
            var profile = new ProfileViewModel();
            profile.about = _user.About;
            profile.firstName = _user.FirstName;
            profile.secondName = _user.SecondName;
            profile.picture = _user.Picture;
            profile.id = _user.Id;

            return profile;
        }

        // GET api/user/1
        public ProfileViewModel Get(string id)
        {
            User _user = userService.GetUser(id);
            var profile = new ProfileViewModel(); //TODO: Use AutoMapper library to map User and ProfileViewModel classes
            profile.about = _user.About;
            profile.firstName = _user.FirstName;
            profile.secondName = _user.SecondName;
            profile.picture = _user.Picture;
            profile.id = _user.Id;

            return profile;
        }

        // POST api/user
        public async Task<HttpResponseMessage> Update(EditProfileViewModel UserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return await this.BadRequest(this.ModelState).ExecuteAsync(new CancellationToken());
            }

            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userId = principal.Claims.Where(c => c.Type == "id").Single().Value;

            User user = userService.GetUser(userId);
            user.FirstName = UserViewModel.FirstName;
            user.About = UserViewModel.About;
            user.SecondName = UserViewModel.SecondName;
            user.Picture = UserViewModel.Picture;
            userService.Update(user);

            IdentityResult result = userService.changePassword(UserViewModel.OldPass, UserViewModel.NewPass, UserViewModel.NewPass2, userId);
            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return await this.GetErrorResult(result).ExecuteAsync(new CancellationToken());
            }


            var responseMsg = new HttpResponseMessage(HttpStatusCode.OK);
            return responseMsg;
            

        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

    }
}
