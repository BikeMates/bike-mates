using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using BikeMates.Contracts.Services;
using BikeMates.Domain.Entities;
using BikeMates.Service.Models;

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
        public IHttpActionResult UnbanUsers(List<string> userId) //TODO: Rename to userIds
        {
            User user;
            userId.RemoveAt(userId.Count - 1); //TODO: Do not remove values. Check if it is not null

            foreach (var id in userId) //TODO: Move this logic to UserService
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
        public IHttpActionResult UnbanRoutes(List<int> routeId) //TODO: Rename to routeIds
        {
            Route route;

            foreach (var id in routeId) //TODO: Move this logic to RouteService
            {
                route = routeService.Find(id);
                route.IsBanned = false;
                routeService.Update(route);
            }
            return Ok();
        }
    }
}
