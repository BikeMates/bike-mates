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
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal; //TODO: Move this logic to the BaseController. Create UserId property there
            var userId = principal.Claims.Single(c => c.Type == "id").Value;

            Mapper.CreateMap<User, ProfileViewModel>();
            User user = userService.GetUser(userId);
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
        public async Task<HttpResponseMessage> Update(EditProfileViewModel userViewModel) //TODO: Rename to editProfileViewModel
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userId = principal.Claims.Single(c => c.Type == "id").Value;

            User user = userService.GetUser(userId); //TODO: Use AutoMapper for mapping
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
