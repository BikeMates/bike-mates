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
    public class RoutesController : ApiController
    {
            private RouteService RouteService;
            private RoutesSearchParameters routesSearchParameters;
            private BikeMates.DataAccess.Repository.RouteRepository routelist = new BikeMates.DataAccess.Repository.RouteRepository(new BikeMatesDbContext());
            public IEnumerable<Route> allRoutes;
            private RouteService routeService;
            public RoutesController()
            {
                routeService = new RouteService(new RouteRepository(new BikeMatesDbContext()));
                allRoutes = routelist.GetAllRoutes();
            }
            [HttpGet]
            public Route Get(int id)
            {
                
                Route rot = new Route();
                //rot.MapData.Start = routeService.GetRoute(1).MapData.Start;
               // rot.MapData.End = routeService.GetRoute(1).MapData.End;
                rot.Participants = routeService.GetRoute(id).Participants;
                rot.Title = routeService.GetRoute(id).Title;
                rot.Start = routeService.GetRoute(id).Start;
                rot.MeetingPlace = routeService.GetRoute(id).MeetingPlace;
                rot.Distance = routeService.GetRoute(id).Distance;
                return rot;
            }

            [HttpPost]
            public IEnumerable<Route> Search(RouteSearch routesearch)//TODO: Use only one method Seacrh which will receive both search and sort options
            {

                //routesSearchParameters.Participants = routesearch.ByParticipants;
                //routesSearchParameters.Name = routesearch.ByName;
                //routesSearchParameters.Date = routesearch.ByDate;
                routesSearchParameters.Date1 = routesearch.Date1;
                routesSearchParameters.Date2 = routesearch.Date2;
                routesSearchParameters.Dist1 = routesearch.Distance1;
                routesSearchParameters.Dist2 = routesearch.Distance2;
                routesSearchParameters.Location = routesearch.Location;
                return RouteService.Find(routesSearchParameters);
            }
            //[HttpGet]
            //public List<Route> GetRoute()
            //{
            //    return allRoutes;
            //}
        }
    }
