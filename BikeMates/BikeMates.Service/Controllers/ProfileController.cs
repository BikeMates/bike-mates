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
    public class ProfileController : BaseController
    {
        private readonly IUserService userService;

        public ProfileController(IUserService userService)
        {
            this.userService = userService;
        }

        // GET api/user
        [HttpGet]
        public ProfileViewModel Get()
        {   //get logged user id
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userId = principal.Claims.Where(c => c.Type == "id").Single().Value;

            AutoMapper.Mapper.CreateMap<User, ProfileViewModel>();
            User user = userService.GetUser(userId);
            var obj = AutoMapper.Mapper.Map<User, ProfileViewModel>(user);
            return AutoMapper.Mapper.Map<User, ProfileViewModel>(user);
        }

        // GET api/user/1
        [HttpGet]
        public ProfileViewModel Get(string id)
        {
            User user = userService.GetUser(id);
            AutoMapper.Mapper.CreateMap<User, ProfileViewModel>();
            return AutoMapper.Mapper.Map<User, ProfileViewModel>(user);
        }

        // POST api/user
        [HttpPost]
        public async Task<HttpResponseMessage> Update(EditProfileViewModel userViewModel)
        {

            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userId = principal.Claims.Where(c => c.Type == "id").Single().Value;

            User user = userService.GetUser(userId); //TODO: Use Automapper for mapping   RESP: creating error when updating entityusing automapper
            user.FirstName = userViewModel.FirstName;
            user.About = userViewModel.About;
            user.SecondName = userViewModel.SecondName;
            user.Picture = userViewModel.Picture;
            userService.Update(user);

            IdentityResult result = userService.ChangePassword(userViewModel.OldPassword, userViewModel.NewPassword, userViewModel.NewPasswordConfirmation, userId);
            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                HttpResponseMessage ia = await this.GetErrorResult(result).ExecuteAsync(new CancellationToken());
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
