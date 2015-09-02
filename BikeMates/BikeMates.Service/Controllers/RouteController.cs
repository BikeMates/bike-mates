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
    public class RouteController : ApiController
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
        public RouteViewModel Find(int id)
        {
            RouteViewModel dto = RouteViewModel.MapToViewModel(routeService.Find(id));
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
            return routeService.Find(id).MapData;
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
        [Route("GetRoutes")]
        public IEnumerable<RouteViewModel> GetRoutes(RouteSearchParametersViewModel search)
        {
            searchParameters = new RouteSearchParameters();
            searchParameters.MeetingPlace = search.Location;

            DateTime dateFrom;
            if (DateTime.TryParse(search.DateFrom, out dateFrom))
            {
                searchParameters.DateFrom = dateFrom;
            }

            DateTime dateTo;
            if (DateTime.TryParse(search.DateTo, out dateTo))
            {
                searchParameters.DateTo = dateTo;
            }

            double minDistance = 0;
            if (double.TryParse(search.MinDistance, out minDistance))
            {
                searchParameters.MinDistance = minDistance;
            }

            double maxDistance = 0;
            if (double.TryParse(search.MaxDistance, out maxDistance))
            {
                searchParameters.MaxDistance = maxDistance;
            }

            RouteSortBy orderByField = RouteSortBy.Date;

            Enum.TryParse<RouteSortBy>(search.OrderByFieldName, true, out orderByField);
            searchParameters.SortOrder = orderByField;

            IEnumerable<Route> routes = routeService.Search(searchParameters).ToArray();

            var routesModels = routes.Select(RouteViewModel.MapToViewModel).ToList();
            return routesModels;
        }

    }
}