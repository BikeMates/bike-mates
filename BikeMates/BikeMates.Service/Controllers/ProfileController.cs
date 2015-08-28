using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
// project reffrerences
using BikeMates.Application.Services;
using BikeMates.Domain.Entities;
using BikeMates.DataAccess.Repository;
using BikeMates.DataAccess;
using BikeMates.Service.Models;
//for identifiying user
using System.Security.Claims;
using Microsoft.AspNet.Identity;
//for async methods 
using System.Threading;
using System.Threading.Tasks;
//for ninject implementation
using BikeMates.Contracts.Services;


//TODO: format code (spaces/tabs, remove blank lines) use - ctrl k, d

namespace BikeMates.Service.Controllers
{
    [RoutePrefix("api/Profile")]
    public class ProfileController : ApiController
    {
        private UserService userService;
        //private IUserService userService; for ninject

        public ProfileController()
        {
          //  userService = userServicein; for ninject
          userService = new UserService(new UserRepository(new BikeMatesDbContext()));
        }

        // GET api/user
        [HttpGet]
        public ProfileViewModel Get()
        {   //get logged user id
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
             var userId = principal.Claims.Where(c => c.Type == "id").Single().Value;
          
            User user = userService.GetUser(userId);
            var profile = new ProfileViewModel();
            profile.About = user.About;
            profile.FirstName = user.FirstName;
            profile.SecondName = user.SecondName;
            profile.Picture = user.Picture;
            profile.Id = user.Id;

            return profile;
        }

        // GET api/user/1
        [HttpGet]
        public ProfileViewModel Get(string id)
        {
            User user = userService.GetUser(id);
            var profile = new ProfileViewModel();
            profile.About = user.About;
            profile.FirstName = user.FirstName;
            profile.SecondName = user.SecondName;
            profile.Picture = user.Picture;
            profile.Id = user.Id;

            return profile;
        }

        // POST api/user
        [HttpPost]
        public async Task<HttpResponseMessage> Update(EditProfileViewModel UserViewModel)
        {
          
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userId = principal.Claims.Where(c => c.Type == "id").Single().Value;

            User user = userService.GetUser(userId);
            user.FirstName = UserViewModel.FirstName;
            user.About = UserViewModel.About;
            user.SecondName = UserViewModel.SecondName;
            user.Picture = UserViewModel.Picture;
            userService.Update(user);

            IdentityResult result = userService.changePassword(UserViewModel.OldPassword, UserViewModel.NewPassword, UserViewModel.NewPasswordConfirmation, userId);
            IHttpActionResult errorResult = GetErrorResult(result);
            
            if (errorResult != null)
            {
                HttpResponseMessage ia = await this.GetErrorResult(result).ExecuteAsync(new CancellationToken());
                return await this.GetErrorResult(result).ExecuteAsync(new CancellationToken());
            }

            var responsemsg = new HttpResponseMessage(HttpStatusCode.BadRequest);
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
