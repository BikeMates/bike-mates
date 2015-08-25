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

namespace BikeMates.Service.Controllers
{
    public class SearchController : ApiController
    {
        private RouteService RouteService;
        private BikeMates.DataAccess.Repository.RouteRepository routelist = new BikeMates.DataAccess.Repository.RouteRepository();
        public List<Route> allRoutes;
        public SearchController()
        {
            allRoutes = routelist.GetAllRoutes().ToList();
            RouteService = new RouteService(new RouteRepository(new BikeMatesDbContext()));

        }
       
        public List<Route> Sort(RouteSort routesort)//TODO: Use only one method Seacrh which will receive both search and sort options
        {
            routesort.ByParticipants = true;
            return RouteService.Sort(allRoutes,routesort.ByDate,routesort.ByName,routesort.ByParticipants);
        }
       /* public List<Route> Search(RouteSearch routelist) {

            return RouteService.Find(allRoutes, routelist.Location, routelist.Distance1, routelist.Distance2, routelist.Date1, routelist.Date2);
        }*/
 
       public List<Route> Get()
        {
            //return allRoutes;
            return allRoutes;
             // return  RouteService.Sort(allRoutes,false,false,true);
        }
        

    }
}