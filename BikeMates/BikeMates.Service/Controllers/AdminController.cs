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
    [RoutePrefix("api/Admin")]
    public class AdminController : ApiController
    {
        private IUserService userService;
        private IRouteService routeService;
        public AdminController()
        {
            userService = new UserService(new UserRepository(new BikeMatesDbContext()));
            routeService = new RouteService(new RouteRepository(new BikeMatesDbContext()));
        }

        [HttpGet]
        [Route("GetBanedUsers")]
        public IHttpActionResult GetBanedUsers()
        {
            List<User> users = userService.GetAll().Where(x => x.IsBanned == true).ToList();
            Mapper.CreateMap<User, UserModel>();
            List<UserModel> model = new List<UserModel>();
            foreach (var user in users)
            {
                model.Add(Mapper.Map<UserModel>(user));
            }
            return Ok(model);
        }

        [HttpGet]
        [Route("GetBanedRoutes")]
        public IHttpActionResult GetBanedRoutes()
        {
            List<Route> routes = new List<Route>();// = routeService.GetAll().Where(x => x.IsBaned == true).ToList();
            routes.Add(new Route(){
                Id = 1,
                Title = "Route #1",
                Description = "Description #1"
            });
            Mapper.CreateMap<Route, RouteModel>();
            List<RouteModel> model = new List<RouteModel>();
            foreach (var route in routes)
            {
                model.Add(Mapper.Map<RouteModel>(route));
            }
            return Ok(model);
        }
    }
}
