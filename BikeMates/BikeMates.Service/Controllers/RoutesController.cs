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
    public class RoutesController : ApiController
    {
        // [RoutePrefix("api/Route")]
       
            private RouteService routeService;
            public RoutesController()
            {
                routeService = new RouteService(new RouteRepository(new BikeMatesDbContext()));

            }
            [HttpGet]
            public Route Get()
            {
                Route rot = new Route();
                rot.Title = routeService.GetRoute(1).Title;
                rot.Start = routeService.GetRoute(1).Start;
                rot.MeetingPlace = routeService.GetRoute(1).MeetingPlace;
                rot.Distance = routeService.GetRoute(1).Distance;
                return rot;
            }

            [HttpPost]
           // [Route("GetRoute")]
            public IHttpActionResult GetRoute(RouteViewModel model) //TODO: Use HttpPost methods to update Routes. And HttpGet to Get information about routes. So remove this method
            {
                model.Title = routeService.GetRoute(1).Title;
                model.Start = routeService.GetRoute(1).Start;
                model.MeetingPlace = routeService.GetRoute(1).MeetingPlace;
                model.Distance = routeService.GetRoute(1).Distance;
                return Ok(model);
            }
        }
    }
