using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BikeMates.Contracts.Services;
using BikeMates.Domain.Entities;
using BikeMates.Service.Models;

namespace BikeMates.Service.Controllers
{
    [RoutePrefix("api/Route")]
    public class RouteController : BaseController
    {   
        private readonly IRouteService routeService;
        private readonly IUserService userService;

        public RouteController(IRouteService routeService, IUserService userService)
        {
            this.routeService = routeService;
            this.userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Find/{id}")]
        public RouteViewModel Find(int id)
        {
            RouteViewModel dto = RouteViewModel.MapToViewModel(routeService.Find(id)); //TODO: Rename dto
            return dto;
        }

        [HttpGet]
        [Route("FindLogged/{id}")]
        public RouteViewModel FindLogged(int id)
        {
            RouteViewModel routeViewModel = RouteViewModel.MapToViewModel(routeService.Find(id)); //TODO: Rename dto
            routeViewModel.IsSubscribed = routeService.CheckIsUserSubscribedToRoute(id, this.UserId);
            return routeViewModel;
        }


        [HttpPost]
        [Route("Update")]
        public void Update(RouteViewModel route)
        {
            Route domainRoute = route.MapToDomain();
            domainRoute.Author = userService.GetUser(this.UserId);
            routeService.Update(domainRoute);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetMapData/{id}")]
        public MapData GetMapData(int id)
        {
            return routeService.Find(id).MapData;
        }

        [HttpPut]
        [Route("Put")]
        public void Put(RouteViewModel route)
        {
            Route domainRoute = route.MapToDomain();
            domainRoute.Author = userService.GetUser(this.UserId);
            routeService.Add(domainRoute);
        }

        [HttpDelete]
        [Route("Delete")]
        public void Delete(int id)
        {
            routeService.Delete(id);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("GetRoutes")]
        public IEnumerable<RouteViewModel> GetRoutes(RouteSearchParametersViewModel search)
        {
            RouteSearchParameters searchParameters = new RouteSearchParameters();
            searchParameters.MeetingPlace = search.Location;

            //TODO: Refactor the code below. Do not parse values replace types in RouteSearchParametersViewModel class
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

            RouteSortOptions orderByField = RouteSortOptions.Date;

            Enum.TryParse<RouteSortOptions>(search.OrderByFieldName, true, out orderByField);
            searchParameters.SortOrder = orderByField;

            IEnumerable<Route> routes = routeService.Search(searchParameters).ToArray();

            var routesModels = routes.Select(RouteViewModel.MapToViewModel).ToList();
            return routesModels;
        }

    }
}