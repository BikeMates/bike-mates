using BikeMates.Application.Services;
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
using System.Web.Mvc;
using Newtonsoft.Json;

namespace BikeMates.Service.Controllers
{
    public class RouteController : ApiController
    {
        // [RoutePrefix("api/Route")]
        private BikeMates.DataAccess.Repository.RouteRepository routelist =
            new BikeMates.DataAccess.Repository.RouteRepository(new BikeMatesDbContext());

        public List<Route> allRoutes;
        private RouteService routeService;

        public RouteController()
        {
            routeService = new RouteService(new RouteRepository(new BikeMatesDbContext()));
            allRoutes = routelist.GetAll().ToList();
        }

        [System.Web.Http.HttpGet]
        public Route Get()
        {

            Route rot = new Route();
            //rot.MapData.Start = routeService.GetRoute(1).MapData.Start;
            // rot.MapData.End = routeService.GetRoute(1).MapData.End;
            rot.Subscribers = routeService.GetRoute(1).Subscribers;
            rot.Title = routeService.GetRoute(1).Title;
            rot.Start = routeService.GetRoute(1).Start;
            rot.MeetingPlace = routeService.GetRoute(1).MeetingPlace;
            rot.Distance = routeService.GetRoute(1).Distance;
            return rot;
        }

        [System.Web.Http.HttpPost]
        // [Route("GetRoute")]
        public RouteViewModel GetRoute(RouteViewModel model)
        {

            model.Title = routeService.GetRoute(1).Title;
            model.Start = routeService.GetRoute(1).Start;
            model.MeetingPlace = routeService.GetRoute(1).MeetingPlace;
            model.Distance = routeService.GetRoute(1).Distance;
            return model;
        }

        [System.Web.Http.HttpPut]
        public void Put(BikeMates.Service.Models.RouteViewModel route, FormCollection form)
        {
            route.MapData = JsonConvert.DeserializeObject<BikeMates.Domain.Entities.MapData>(form["MapData"]);
            route.Distance = Double.Parse(form["Distance"], System.Globalization.NumberStyles.AllowDecimalPoint,
                System.Globalization.NumberFormatInfo.InvariantInfo);
            routeService.Add(route.MapToDomain());
        }

        [System.Web.Http.HttpDelete]
        public void Delete(int id)
        {
            routeService.Delete(id);
        }
    }
}
