using AutoMapper;
using BikeMates.Contracts.Services;
using BikeMates.Domain.Entities;
using BikeMates.Service.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
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
        public async Task<ValidationResponseViewModel> Update(EditProfileViewModel editProfileViewModel) 
        {
            User user = userService.GetUser(this.UserId); 
            List <string> passwordErrors = new List<string>();
            List<string> information = new List<string>();
            List<string> nameErrors = new List<string>();
            passwordErrors = (List<string>)userService.ChangePassword(editProfileViewModel.OldPassword, editProfileViewModel.NewPassword, editProfileViewModel.NewPasswordConfirmation, this.UserId);
            nameErrors = (List<string>)userService.CheckUserName(editProfileViewModel.FirstName, editProfileViewModel.SecondName);
            if (nameErrors.Count == 0)
            {
                information = (List<string>)userService.CheckUserInfo(editProfileViewModel.FirstName, editProfileViewModel.SecondName, editProfileViewModel.About, this.UserId);
                if (passwordErrors.Count == 0 && !String.IsNullOrWhiteSpace(editProfileViewModel.NewPassword) && !String.IsNullOrWhiteSpace(editProfileViewModel.OldPassword) && !String.IsNullOrWhiteSpace(editProfileViewModel.NewPasswordConfirmation))
                    {
                        information.Add("Password changed");
                    }

                user.FirstName = editProfileViewModel.FirstName;
                user.About = editProfileViewModel.About;
                user.SecondName = editProfileViewModel.SecondName;
                userService.Update(user);
            }
            return new ValidationResponseViewModel(passwordErrors, information , nameErrors);
        }
    }
}
