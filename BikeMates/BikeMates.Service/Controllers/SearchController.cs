using BikeMates.Application.Services;
using BikeMates.Contracts.Services;
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

namespace BikeMates.Service.Controllers
{
    public class SearchController : ApiController //TODO: Remove this controller. Move logic to the RoutesController
    {
        private RouteService RouteService;
        private BikeMates.DataAccess.Repository.RouteRepository routelist = new BikeMates.DataAccess.Repository.RouteRepository(new BikeMatesDbContext());
        public IEnumerable<Route> allRoutes;
        private RouteSearchParameters routeSearchParameters;
        private IEnumerable<Route> ListSort;
        public SearchController()
        {
            allRoutes = routelist.GetAll().ToList();
          //  RouteSearchParameters.entity = routelist.GetAllRoutes();
            RouteService = new RouteService(new RouteRepository(new BikeMatesDbContext()));
        }

       [HttpGet]
       public IEnumerable<Route> Get()
        {           
            return allRoutes;           
        }
       [HttpGet]
       public IEnumerable<Route> Get(int id) {
           
           if (id == 1)
           {
               var routesSort = from nameRoute
                            in allRoutes
                           orderby nameRoute.Title
                           select nameRoute;
               ListSort =  routesSort;
           }
           if (id == 2)
           {
               var routesSort = from nameRoute
                                in allRoutes
                                orderby nameRoute.Subscribers.Count
                                select nameRoute;
               ListSort = routesSort;
           }
           if (id == 3)
           {
               var routesSort = from nameRoute
                                in allRoutes
                                orderby nameRoute.Start
                                select nameRoute;
               ListSort = routesSort;
           }
           return ListSort ;      
       }
    }
}