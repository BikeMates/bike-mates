using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BikeMates.Application.Services;
using BikeMates.Contracts.Services;
using BikeMates.DataAccess;
using BikeMates.DataAccess.Repository;
using Newtonsoft.Json;

namespace BikeMates.Web.Controllers
{
    public class RouteController : Controller //TODO: Remove this controller
    {

        private readonly IRouteService routeService;

        public RouteController(IRouteService routeService)
        {
            this.routeService = routeService;
        }
        public ActionResult Add()
        {
            var route = new BikeMates.Service.Models.RouteViewModel();
            return View(route);
        }
        public void Save(BikeMates.Service.Models.RouteViewModel route) //TODO: Move this logic to the WebApi controller
        {
            routeService.Add(route.MapToDomain());
        }
    }
}