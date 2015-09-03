﻿using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using BikeMates.Contracts.Services;
using BikeMates.Domain.Entities;

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

        [HttpGet]
        [AllowAnonymous]
        public bool GetIsUserSubscribedToRoute(int id)
        {
            var principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            //string userId = principal.Claims.Single(c => c.Type == "id").Value;
            string userId = "be44b720-b9e0-4c17-ae83-0fb8e5beecf7";
            User user = userService.GetUser(userId);
            Route route = routeService.Find(id);

            bool isSubscribed = route.Subscribers.Contains(user);
            return isSubscribed;
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

            int routeId = id;
            User user = userService.GetUser(userId);
            Route route = routeService.Find(routeId);
            this.routeService.UnsubscribeUser(route, user);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
