using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BikeMates.Application.Services;
using BikeMates.DataAccess;
using BikeMates.DataAccess.Repository;
using Newtonsoft.Json;

namespace BikeMates.Web.Controllers
{
    public class RouteController : Controller
    {

        private RouteService routeService;

        public RouteController()
        {
            routeService = new RouteService(new RouteRepository(new BikeMatesDbContext()));
        }
        
        public ActionResult Add()
        {
            var route = new Web.Models.Route();
            return View(route);
        }
        public void Save(BikeMates.Web.Models.Route route, FormCollection form)
        {
            route.MapData = JsonConvert.DeserializeObject<BikeMates.Domain.Entities.MapData>(form["MapData"]);
            route.Start = DateTime.Now;
            route.Distance = Double.Parse(form["Distance"], System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);
            routeService.Add(route.MapToDomain());
        }
    }
}