using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using BikeMates.Contracts.Data;
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
        private int ID;

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
            RouteViewModel routeViewModel = RouteViewModel.MapToViewModel(routeService.Find(id));
            return routeViewModel;
        }

        [HttpGet]
        [Route("FindLogged/{id}")]
        public RouteViewModel FindLogged(int id)
        {
            RouteViewModel routeViewModel = RouteViewModel.MapToViewModel(routeService.Find(id));
            routeViewModel.IsSubscribed = routeService.CheckIsUserSubscribedToRoute(id, this.UserId);
            return routeViewModel;
        }


        [HttpPut]
        [Route("Update")]
        public async Task<HttpResponseMessage> Update(RouteViewModel route)
        {
            Route domainRoute = routeService.Find(route.Id);
            if (domainRoute.Author != userService.GetUser(this.UserId))
            {
                return await StatusCode(HttpStatusCode.Forbidden).ExecuteAsync(new CancellationToken());
            } 
            Route temporaryObject = RouteViewModel.MapToDomain(route);
            domainRoute.Description = route.Description;
            domainRoute.MeetingPlace = route.MeetingPlace;
            domainRoute.Title = route.Title;
            domainRoute.Start = temporaryObject.Start;
            domainRoute.Distance = temporaryObject.Distance;
            domainRoute.MapData = temporaryObject.MapData;

            routeService.Update(domainRoute);
            return await StatusCode(HttpStatusCode.OK).ExecuteAsync(new CancellationToken());
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetMapData/{id}")]
        public MapData GetMapData(int id)
        {
            return routeService.Find(id).MapData;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ReturnId")]
        public int ReturnId()
        {
            return ID;
        }


        [HttpPost]
        [Route("Add")]
        public void Put(RouteViewModel route)
        {
            Route domainRoute = route.MapToDomain();
            domainRoute.Author = userService.GetUser(this.UserId);
            routeService.Add(domainRoute);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public void Delete(int id)
        {
            routeService.Delete(id);
        }

        [HttpGet]
        [Authorize]
        [Route("RetUserId")]
        public IEnumerable<RouteViewModel> RetUserId([FromUri]RouteSearchParametersViewModel search)
        {
            var searchParameters = new RouteSearchParameters();
            searchParameters.AuthorId = this.UserId;
            
            var orderByField = RouteSortOptions.AllRoutes;

            if (Enum.TryParse<RouteSortOptions>(search.OrderByFieldName, true, out orderByField))
            {
                searchParameters.SortOrder = orderByField;
            }


            searchParameters.PageNumber = 0;
            searchParameters.PageSize = 5000;
            IEnumerable<RouteData> routes = routeService.Search(searchParameters).ToArray();

            var routesModels = routes.Select(RouteViewModel.MapToSearchViewModel).ToList();
            return routesModels;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetRoutes")]
        public IEnumerable<RouteViewModel> GetRoutes([FromUri]RouteSearchParametersViewModel search)
        {
            var searchParameters = new RouteSearchParameters();
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
           
            var orderByField = RouteSortOptions.Date;

            if (Enum.TryParse<RouteSortOptions>(search.OrderByFieldName, true, out orderByField))
            {
                searchParameters.SortOrder = orderByField;
            }


            searchParameters.PageNumber = 0;
            searchParameters.PageSize = 5000;
            IEnumerable<RouteData> routes = routeService.Search(searchParameters).ToArray();

            var routesModels = routes.Select(RouteViewModel.MapToSearchViewModel).ToList();
            return routesModels;
        }

    }
}