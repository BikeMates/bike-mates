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
        private List<Route> allRoutes;
        public SearchController()
        {
            allRoutes = routelist.GetAllRoutes().ToList();
            RouteService = new RouteService(new RouteRepository(new BikeMatesDbContext()));

        }
        // POST api/user
        public void Update(RouteSearch routelist)
        {

            routelist.Location = "wololo";
            routelist.Date1 = "wololo";
            routelist.Date2 = "wololo";
            routelist.Distance1 = "wololo";
            routelist.Distance2 = "wololo";
            foreach (Route rot in allRoutes)
            {
                rot.Name = "SuccessEvent";
                rot.ParticipantsCount = 200;
            }
        }

        public List<Route> Get()
        {
            return allRoutes;
        }


    }
}