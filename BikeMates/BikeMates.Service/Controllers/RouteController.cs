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
        public IEnumerable<Route> Searching(RouteSearch routesearch) {
            searchParameters=new RouteSearchParameters();
            if (routesearch.Location.Length !=0)
            {
                searchParameters.MeetingPlace = routesearch.Location;
            }
            if (routesearch.DateTo.Length != 0)
            {
                searchParameters.DateTo = DateTime.Parse(routesearch.DateTo);
            }
            if (routesearch.DateFrom.Length != 0)
            {
                searchParameters.DateFrom = DateTime.Parse(routesearch.DateFrom);
            }
            if (routesearch.MaxDistance.Length != 0)
            {
                searchParameters.MaxDistance = Int32.Parse(routesearch.MaxDistance);
            }
            if (routesearch.MinDistance.Length != 0)
            {
                searchParameters.MinDistance = Int32.Parse(routesearch.MinDistance);
            }
            return routeService.Search(searchParameters);     
        }
        [HttpGet]
        [Route("GetRoutes")]
        public IEnumerable<Route> Get()
        {
            return allRoutes;
        }
        [HttpGet]
        [Route("SortTitle")]
        public IEnumerable<Route> SortTitle()
        {
                    var routesSort = from nameRoute
                             in allRoutes
                                 orderby nameRoute.Title
                                 select nameRoute;             
            return routesSort;
        }
        [HttpGet]
        [Route("SortSubscribes")]
        public IEnumerable<Route> SortSubscribes()
        {
                var routesSort = from nameRoute
                                 in allRoutes
                                 orderby nameRoute.Subscribers.Count
                                 select nameRoute;
                return routesSort;
            }
        [HttpGet]
        [Route("SortDate")]
        public IEnumerable<Route> SortDate()
        {
                var routesSort = from nameRoute
                                 in allRoutes
                                 orderby nameRoute.Start
                                 select nameRoute;
                return routesSort;
            }       
        }
    }

