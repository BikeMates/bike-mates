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
using Newtonsoft.Json;
using BikeMates.Web.Models;
using System.Net;

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
            user.Role = "User";
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

        [HttpPost]
        [Route("ForgotPassword")]
        public IHttpActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (!checkCaptcha(model.Response).Success)
            {
                return BadRequest("Invalid captcha");
            }

            var user = userService.getUserByEmail(model.Email);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            userService.forgotPassword(user.Id, model.Host);

            return Ok();
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<HttpResponseMessage> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return await BadRequest(this.ModelState).ExecuteAsync(new CancellationToken());
            }

            var user = userService.getUserByEmail(model.Email);

            if (user == null)
            {
                return await BadRequest("User not found").ExecuteAsync(new CancellationToken());
            }

            var result = userService.resetPassword(user.Id, model.Code, model.Password);
            var errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return await errorResult.ExecuteAsync(new CancellationToken());
            }
            return await Ok().ExecuteAsync(new CancellationToken());
        }

        private CaptchaModel checkCaptcha(string response)
        {
            const string secret = "6LdnvQsTAAAAAGM8ZQ8kr46eAalzSBzH_BpnYoN3";

            var webClient = new WebClient();
            var reply =
                webClient.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            return JsonConvert.DeserializeObject<CaptchaModel>(reply);
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



        private IHttpActionResult GetErrorResult(IdentityResult result) //TODO: Create BaseApiController and move this method into it
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