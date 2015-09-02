using System;
using System.Linq;
using BikeMates.Application.Services;
using BikeMates.DataAccess;
using BikeMates.DataAccess.Repository;
using BikeMates.Domain.Entities;
using BikeMates.Service.Models;
using System.Web.Http;
using BikeMates.Contracts.Services;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BikeMates.Service.Controllers
{
    [RoutePrefix("api/Route")]
    public class RouteController : BaseController
    {
        private RouteSearchParameters searchParameters;        
        public IEnumerable<Route> allRoutes;
        private readonly IRouteService routeService;
        
        public RouteController(IRouteService routeService)
        {
            this.routeService = routeService;
            allRoutes = routeService.GetAll();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Get/{id}")]
        public RouteViewModel Get(int id)
        {  
            RouteViewModel dto = RouteViewModel.MapToViewModel(routeService.Get(id));
            return dto;
        }

        [HttpPost]
        [Route("Update")]
        public RouteViewModel Update(RouteViewModel route)
        {
            routeService.Update(route.MapToDomain());
            return route;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetMapData/{id}")]
        public MapData GetMapData(int id)
        {
            return routeService.Get(id).MapData;
        }

        [HttpPut]
        [AllowAnonymous]
        [Route("Put")]
        public void Put(RouteViewModel route)
        {
            routeService.Add(route.MapToDomain());
        }

        [HttpDelete]
        [Route("Delete")]
        public void Delete(int id)
        {
            routeService.Delete(id);
        }
        [HttpPost]
        [Route("Search")]
        public IEnumerable<RouteViewModel> Searching(RouteSearch search)
        {
            searchParameters = new RouteSearchParameters();
            if (search.Location.Length != 0)
            {
                searchParameters.MeetingPlace = search.Location;
            }
            if (search.DateTo.Length != 0)
            {
                searchParameters.DateTo = DateTime.Parse(search.DateTo);
            }
            if (search.DateFrom.Length != 0)
            {
                searchParameters.DateFrom = DateTime.Parse(search.DateFrom);
            }
            if (search.MaxDistance.Length != 0)
            {
                searchParameters.MaxDistance = Int32.Parse(search.MaxDistance);
            }
            if (search.MinDistance.Length != 0)
            {
                searchParameters.MinDistance = Int32.Parse(search.MinDistance);
            }
            var allroutescopy = routeService.Search(searchParameters);
            var routes = new List<RouteViewModel>();
            foreach (var item in allroutescopy)
            {
                routes.Add(RouteViewModel.MapToViewModel(item));
            }
            return routes;
        }
        [HttpGet]
        [Route("GetRoutes")]
        public IEnumerable<RouteViewModel> Get()
        {
            var viewroute = new List<RouteViewModel>();
            foreach (var item in allRoutes.ToArray())
            {
                viewroute.Add(RouteViewModel.MapToViewModel(item));
            }
            var routes = viewroute;
            return routes;
        }
        [HttpGet]
        [Route("SortTitle")]
        public IEnumerable<RouteViewModel> SortTitle()
        {
            var routesSort = from nameRoute
                     in allRoutes
                             orderby nameRoute.Title
                             select nameRoute;
            var routeSort = new List<RouteViewModel>();
            foreach (var item in routesSort)
            {
                routeSort.Add(RouteViewModel.MapToViewModel(item));
            }
            return routeSort;
        }
        [HttpGet]
        [Route("SortSubscribes")]
        public IEnumerable<RouteViewModel> SortSubscribes()
        {
            var routesSort = from nameRoute
                             in allRoutes
                             orderby nameRoute.Subscribers.Count
                             select nameRoute;
            var routeSort = new List<RouteViewModel>();
            foreach (var item in routesSort)
            {
                routeSort.Add(RouteViewModel.MapToViewModel(item));
            }
            return routeSort;
        }
        [HttpGet]
        [Route("SortDate")]
        public IEnumerable<RouteViewModel> SortDate()
        {
            var routesSort = from nameRoute
                             in allRoutes
                             orderby nameRoute.Start
                             select nameRoute;
            var routeSort = new List<RouteViewModel>();
            foreach (var item in routesSort)
            {
                routeSort.Add(RouteViewModel.MapToViewModel(item));
            }
            return routeSort;
        }
        [HttpGet]
        [Route("ViewModel")]
        public RouteViewModel ViewModel(int id)
        {
            RouteViewModel rot = new RouteViewModel();
            //rot.MapData.Start = routeService.GetRoute(1).MapData.Start;
            // rot.MapData.End = routeService.GetRoute(1).MapData.End;
            // rot.Subscribers = routeService.Get(id).Subscribers;
            rot.Title = routeService.Get(id).Title;
            rot.Start = routeService.Get(id).Start;
            rot.MeetingPlace = routeService.Get(id).MeetingPlace;
            rot.Distance = (routeService.Get(id).Distance).ToString();
            return rot;
        }
    }
}