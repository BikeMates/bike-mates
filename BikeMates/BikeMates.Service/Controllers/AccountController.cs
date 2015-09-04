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
using System.Net;
using BikeMates.Contracts.Services;

namespace BikeMates.Service.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : BaseController
    {
        private IUserService userService;
        private ICaptchaService captchaService;

        public AccountController(IUserService userService, ICaptchaService captchaService)
        {
            this.userService = userService;
            this.captchaService = captchaService;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<HttpResponseMessage> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return await this.BadRequest(this.ModelState).ExecuteAsync(new CancellationToken());
            }

            Mapper.CreateMap<RegisterViewModel, User>();
            var user = Mapper.Map<User>(registerViewModel);
            user.UserName = registerViewModel.Email;
            user.Role = "User";
            IdentityResult result = userService.Register(user, registerViewModel.Password);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return await this.GetErrorResult(result).ExecuteAsync(new CancellationToken());
            }
            var userLoginModel = new LoginViewModel
            {
                Email = registerViewModel.Email,
                Password = registerViewModel.Password
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

                if (responseMsg.StatusCode == HttpStatusCode.OK) 
                {
                    var auth = getAuthData(userModel, responseString);
                    return await Ok<AuthModel>(auth).ExecuteAsync(new CancellationToken());
                }
                return responseMsg;
            }
        }

        private AuthModel getAuthData(LoginViewModel userModel, string responseString)
        {
            var loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(responseString);
            var auth = new AuthModel();
            var user = userService.GetUserByEmail(userModel.Email);

            auth.Token = loginResponse.access_token;
            auth.Role = user.Role;
            auth.FirstName = user.FirstName;
            auth.SecondName = user.SecondName;
            auth.IsAuthorized = true;
            return auth;
        }

        

        [HttpPost]
        [AllowAnonymous]
        [Route("ForgotPassword")]
        public IHttpActionResult ForgotPassword(ForgotPasswordModel model)
        {
            if (!captchaService.checkCaptcha(model.Response))
            {
                return BadRequest("Invalid captcha");
            }

            var user = userService.GetUserByEmail(model.Email);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            userService.ForgotPassword(user.Id, model.Host);

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<HttpResponseMessage> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return await BadRequest(this.ModelState).ExecuteAsync(new CancellationToken());
            }

            var user = userService.GetUserByEmail(model.Email);

            if (user == null)
            {
                return await BadRequest("User not found").ExecuteAsync(new CancellationToken());
            }

            var result = userService.ResetPassword(user.Id, model.Code, model.Password);
            var errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return await errorResult.ExecuteAsync(new CancellationToken());
            }
            return await Ok().ExecuteAsync(new CancellationToken());
        }

        

        [HttpPost]
        [Route("GetUserByEmail")]
        public IHttpActionResult GetUserByEmail(ForgotPasswordModel model)
        {
            Mapper.CreateMap<User, UserModel>();
            var user = userService.GetUserByEmail(model.Email);
            UserModel userModel = Mapper.Map<UserModel>(user);
            return Ok(userModel);
        }       
    }
}