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
    public class SearchController : ApiController
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
       
        public List<Route> Search(RouteSearch routesearch)//TODO: Use only one method Seacrh which will receive both search and sort options
        {
           
            routesSearchParameters.Participants = routesearch.ByParticipants;
            routesSearchParameters.Name = routesearch.ByName;
            routesSearchParameters.Date = routesearch.ByDate;
            routesSearchParameters.Date1 = routesearch.Date1;
            routesSearchParameters.Date2 = routesearch.Date2;
            routesSearchParameters.Dist1 = routesearch.Distance1;
            routesSearchParameters.Dist2 = routesearch.Distance2;
            routesSearchParameters.Location = routesearch.Location;
            return RouteService.Find(routesSearchParameters);
        }

       public List<Route> Get()
        {
            
            return allRoutes;
            
        }
        

    }
}