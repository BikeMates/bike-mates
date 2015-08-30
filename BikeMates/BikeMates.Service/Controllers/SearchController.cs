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
        public List<Route> allRoutes;
        private RoutesSearchParameters routesSearchParameters;
        public SearchController()
        {
            allRoutes = routelist.GetAllRoutes();
          //  routesSearchParameters.entity = routelist.GetAllRoutes();
            RouteService = new RouteService(new RouteRepository(new BikeMatesDbContext()));

        }

       [HttpGet]
       public List<Route> Get()
        {
            
            return allRoutes;
            
        }
        

    }
}