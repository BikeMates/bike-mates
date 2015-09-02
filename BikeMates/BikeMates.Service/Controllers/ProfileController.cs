using BikeMates.Application.Services;
using BikeMates.Contracts.Services;
using BikeMates.DataAccess;
using BikeMates.DataAccess.Repository;
using BikeMates.Domain.Entities;
using BikeMates.Service.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

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
        {   ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userId = principal.Claims.Single(c => c.Type == "id").Value;

            AutoMapper.Mapper.CreateMap<User, ProfileViewModel>();
            User user = userService.GetUser(userId);
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
            var userId = principal.Claims.Single(c => c.Type == "id").Value;

            User user = userService.GetUser(userId); 
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

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
