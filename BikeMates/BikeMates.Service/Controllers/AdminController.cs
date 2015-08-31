using AutoMapper;
using BikeMates.Application.Services;
using BikeMates.Contracts.Services;
using BikeMates.DataAccess;
using BikeMates.DataAccess.Repository;
using BikeMates.Domain.Entities;
using BikeMates.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BikeMates.Service.Controllers
{
    public class AdminController : ApiController
    {
        private IUserService userService;

        public AdminController()
        {
            userService = new UserService(new UserRepository(new BikeMatesDbContext()));
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetBanedUsers()
        {
            List<User> users = userService.GetAll().Where(x => x.IsBaned == true).ToList();
            Mapper.CreateMap<User,UserModel>();
            List<UserModel> model = new List<UserModel>();
            foreach (var user in users)
            {
                model.Add(Mapper.Map<UserModel>(user));
            }
            return Ok(model);
        }
    }
}
