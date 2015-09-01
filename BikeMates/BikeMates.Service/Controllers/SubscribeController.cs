using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
// project reffrerences
using BikeMates.Application.Services;
using BikeMates.Domain.Entities;
using BikeMates.DataAccess.Repository;
using BikeMates.DataAccess;
using BikeMates.Service.Models;
//for identifiying user
using System.Security.Claims;
using Microsoft.AspNet.Identity;
//for async methods 
using System.Threading;
using System.Threading.Tasks;
//for ninject implementation
using BikeMates.Contracts.Services;

namespace BikeMates.Service.Controllers
{
    public class SubscribeController : ApiController
    {
        private UserService userService;
        private RouteService routeService;

        public SubscribeController()
        {
            routeService = new RouteService(new RouteRepository(new BikeMatesDbContext()));
            userService = new UserService(new UserRepository(new BikeMatesDbContext()));
        }

        [HttpPost]
        public HttpResponseMessage Subscribe(int routeId)
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userId = principal.Claims.Single(c => c.Type == "id").Value;


            var user = userService.GetUser(userId);
            var route = routeService.GetRoute(routeId);

            routeService.SubscribeUser(route, user);
            userService.SubscribeRoute(route, user);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        
        }

        [HttpPost]
        public HttpResponseMessage Unsubcsribe(int routeId)
        {
            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var userId = principal.Claims.Single(c => c.Type == "id").Value;


            var user = userService.GetUser(userId);
            var route = routeService.GetRoute(routeId);

            routeService.UnsubscribeUser(route, user);
            userService.UnsubscribeRoute(route, user);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            return response;

        }



    }
}
