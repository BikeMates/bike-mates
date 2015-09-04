using System.Linq;
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
        private readonly IUserService userService; //TODO: Remove if it is not used
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
           
            string userId = "be44b720-b9e0-4c17-ae83-0fb8e5beecf7";// for test purposes
            int routeId = id;

            return routeService.CheckIsUserSubscribedToRoute(routeId, userId);
        }


        [HttpPut]
        public HttpResponseMessage Subscribe(int id)
        {
            int routeId = id;
            this.routeService.SubscribeUser(routeId, this.userId);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage Unsubscribe(int id)
        {
            int routeId = id;
            this.routeService.UnsubscribeUser(routeId, this.userId);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
