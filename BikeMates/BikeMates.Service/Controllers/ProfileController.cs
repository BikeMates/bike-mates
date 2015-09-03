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
        public profileViewModel Get()
        {  
            Mapper.CreateMap<User, profileViewModel>();
            User user = userService.GetUser(this.userID);
            return Mapper.Map<User, profileViewModel>(user);
        }

        // GET api/user/1
        [HttpGet]
        public profileViewModel Get(string id)
        {
            User user = userService.GetUser(id);
            Mapper.CreateMap<User, profileViewModel>();
            return Mapper.Map<User, profileViewModel>(user);
        }

        // POST api/user
        [HttpPost]
        public async Task<HttpResponseMessage> Update(editProfileViewModel userViewModel) //TODO: Rename to editProfileViewModel
        {

            User user = userService.GetUser(this.userID); //TODO: Use AutoMapper for mapping
            user.FirstName = userViewModel.FirstName;
            user.About = userViewModel.About;
            user.SecondName = userViewModel.SecondName;
            user.Picture = userViewModel.Picture;
            userService.Update(user);

            IdentityResult result = userService.ChangePassword(userViewModel.OldPassword, userViewModel.NewPassword, userViewModel.NewPasswordConfirmation, this.userID);
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
