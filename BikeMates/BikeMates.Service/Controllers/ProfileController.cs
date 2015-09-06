using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BikeMates.Contracts.Services;
using BikeMates.Domain.Entities;
using BikeMates.Service.Models;
using Microsoft.AspNet.Identity;

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
        {  
            Mapper.CreateMap<User, ProfileViewModel>();
            User user = userService.GetUser(this.UserId);
            return Mapper.Map<User, ProfileViewModel>(user);
        }

        // GET api/user/1
        [HttpGet]
        public ProfileViewModel Get(string id)
        {
            User user = userService.GetUser(id);
            Mapper.CreateMap<User, ProfileViewModel>();
            return Mapper.Map<User, ProfileViewModel>(user);
        }

        // POST api/user
        [HttpPost]
        public async Task<HttpResponseMessage> Update(EditProfileViewModel editProfileViewModel) 
        {

            User user = userService.GetUser(this.UserId); //TODO: Use AutoMapper for mapping

            user.FirstName = editProfileViewModel.FirstName;
            user.About = editProfileViewModel.About;
            user.SecondName = editProfileViewModel.SecondName;
            user.Picture = editProfileViewModel.Picture;

            userService.Update(user);

            IdentityResult result = userService.ChangePassword(editProfileViewModel.OldPassword, editProfileViewModel.NewPassword, editProfileViewModel.NewPasswordConfirmation, this.UserId);
            IHttpActionResult errorResult = GetErrorResult(result);
            IdentityResult ir = userService.CheckUserInfo(user);
          
            if (errorResult != null)
            {
                HttpResponseMessage ia = await this.GetErrorResult(result).ExecuteAsync(new CancellationToken());
                return await this.GetErrorResult(result).ExecuteAsync(new CancellationToken());
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
