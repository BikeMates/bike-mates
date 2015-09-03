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
    [Authorize(Roles = "Admin")]
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
        public IHttpActionResult UnbanUsers(List<string> userIds)
        {
            userService.UnbanUsers(userIds);
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
        public IHttpActionResult UnbanRoutes(List<int> routeIds)
        {

            return Ok();
        }
    }
}
