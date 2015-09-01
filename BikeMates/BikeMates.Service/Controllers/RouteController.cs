using System;
using BikeMates.Application.Services;
using BikeMates.DataAccess;
using BikeMates.DataAccess.Repository;
using BikeMates.Domain.Entities;
using BikeMates.Service.Models;
using System.Web.Http;
using Newtonsoft.Json;

namespace BikeMates.Service.Controllers
{
    [RoutePrefix("api/Route")]
    public class RouteController : ApiController
    {
        private RouteService routeService;

        public RouteController()
        {
            routeService = new RouteService(new RouteRepository(new BikeMatesDbContext()));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Get/{id}")]
        public RouteViewModel Get(int id)
        {  
            RouteViewModel dto = RouteViewModel.MapToViewModel(routeService.Get(id));
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
            return routeService.Get(id).MapData;
        }

        [HttpPut]
        [Route("Put")]
        public Route Put(RouteViewModel route)
        {
            return routeService.Add(route.MapToDomain());
        }

        [HttpDelete]
        [Route("Delete")]
        public void Delete(int id)
        {
            routeService.Delete(id);
        }
    }
}
