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
    public class AdminController : BaseController
    {
        private IUserService userService;
        private IRouteService routeService;

        public AdminController(IUserService userService, IRouteService routeService)
        {
            this.userService = userService;
            this.routeService = routeService;
        }

        [HttpGet]
        [Route("GetBannedUsers")]
        public IHttpActionResult GetBannedUsers()
        {
            List<User> users = userService.GetAll().Where(x => x.IsBanned).ToList();
            Mapper.CreateMap<User, UserModel>();
            List<UserModel> model = new List<UserModel>();
            foreach (var user in users)
            {
                model.Add(Mapper.Map<UserModel>(user));
            }
            return Ok(model);
        }

        [HttpPost]
        [Route("UnbanUsers")]
        public IHttpActionResult UnbanUsers(List<string> userId)
        {
            User user;
            userId.RemoveAt(userId.Count - 1);

            foreach (var id in userId)
            {
                user = userService.GetUser(id);
                user.IsBanned = false;
                userService.Update(user);
            }
            return Ok();
        }

        [HttpGet]
        [Route("GetBannedRoutes")]
        public IHttpActionResult GetBannedRoutes()
        {
            List<Route> routes  = routeService.GetAll().Where(x => x.IsBanned).ToList();
            Mapper.CreateMap<Route, RouteModel>();
            List<RouteModel> model = new List<RouteModel>();
            foreach (var route in routes)
            {
                model.Add(Mapper.Map<RouteModel>(route));
            }
            return Ok(model);
        }


        [HttpPost]
        [Route("UnbanRoutes")]
        public IHttpActionResult UnbanRoutes(List<int> routeId)
        {
            Route route;

            foreach (var id in routeId)
            {
                route = routeService.Get(id);
                route.IsBanned = false;
                routeService.Update(route);
            }
            return Ok();
        }
    }
}
