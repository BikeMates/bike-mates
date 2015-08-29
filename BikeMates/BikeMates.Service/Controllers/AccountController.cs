using BikeMates.Application.Services;
using BikeMates.DataAccess;
using BikeMates.DataAccess.Repository;
using BikeMates.Domain.Entities;
using BikeMates.Service.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;

namespace BikeMates.Service.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private UserService userService;

        public AccountController()
        {
            userService = new UserService(new UserRepository(new BikeMatesDbContext()));

        }

        [HttpPost]
        [Route("GetUserByEmail")]
        public IHttpActionResult GetUserByEmail(ForgotPasswordModel model)
        {
            Mapper.CreateMap<User, UserModel>();
            var user = userService.getUserByEmail(model.Email);
            UserModel userModel = Mapper.Map<UserModel>(user);
            return Ok(userModel);
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<HttpResponseMessage> Register(RegisterViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return await this.BadRequest(this.ModelState).ExecuteAsync(new CancellationToken());
            }

            Mapper.CreateMap<RegisterViewModel, User>();
            var user = Mapper.Map<User>(userModel);
            user.UserName = userModel.Email;
            user.Role = "user";
            IdentityResult result = userService.Register(user, userModel.Password);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return await this.GetErrorResult(result).ExecuteAsync(new CancellationToken());
            }
            var userLoginModel = new LoginViewModel
            {
                Email = userModel.Email,
                Password = userModel.Password
            };
            var resultLogin = await Login(userLoginModel);
            return resultLogin;
        }

        [AllowAnonymous]
        [Route("Login")]
        public async Task<HttpResponseMessage> Login(LoginViewModel userModel)
        {
            var request = HttpContext.Current.Request;
            var tokenServiceUrl = request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath + "/token";
            
            using (var client = new HttpClient())
            {
                var requestParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", userModel.Email),
                    new KeyValuePair<string, string>("password", userModel.Password)
                };
                var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
                var tokenServiceResponse = await client.PostAsync(tokenServiceUrl, requestParamsFormUrlEncoded);
                var responseString = await tokenServiceResponse.Content.ReadAsStringAsync();
                var responseCode = tokenServiceResponse.StatusCode;
                var responseMsg = new HttpResponseMessage(responseCode)
                {
                    Content = new StringContent(responseString, Encoding.UTF8, "application/json")
                };
                return responseMsg;
            }
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
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}