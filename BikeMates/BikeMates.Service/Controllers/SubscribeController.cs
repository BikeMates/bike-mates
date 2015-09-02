using BikeMates.Application.Services;
using BikeMates.Contracts.Services;
using BikeMates.DataAccess;
using BikeMates.DataAccess.Repository;
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
    public class SubscribeController : BaseController
    {
        private readonly IUserService userService;
        private readonly IRouteService routeService;

        public SubscribeController(IUserService userService, IRouteService routeService)
        {
            this.userService = userService;
            this.routeService = routeService;
        }

        [HttpPut]
        public HttpResponseMessage Subscribe(int id)
        {
            var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            string userId = principal.Claims.Single(c => c.Type == "id").Value;

            int routeId = id;
            User user = userService.GetUser(userId);
            Route route = routeService.Find(routeId);
            this.routeService.SubscribeUser(route, user);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage Unsubscribe(int id)
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            string userId = principal.Claims.Single(c => c.Type == "id").Value;

            int   routeId = id;
            User  user = userService.GetUser(userId);
            Route route = routeService.Find(routeId);
            this.routeService.UnsubscribeUser(route, user);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
