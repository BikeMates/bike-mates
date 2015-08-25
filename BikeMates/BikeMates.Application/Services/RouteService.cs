using BikeMates.Contracts;
using BikeMates.Contracts.Repositories;
using BikeMates.Contracts.Services;
using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Application.Services
{
    public class RouteService : IRouteService
    {
        private IRouteRepository routeRepository;
        public void Add(Route entity) //TODO: Place this method after constructor
        {
            this.routeRepository.Add(entity);
        }
        public RouteService(IRouteRepository repository) //TODO: rename to routeRepository
        {
            this.routeRepository = repository;
        }
        public Route GetRoute(int id)
        {
            return this.routeRepository.Get(id);
        }
        public void Delete(int id)
        {
            var user = GetRoute(id);//TODO: Move this logic into repository. You should not load Route from database for delete operation
            routeRepository.Delete(user);
        }

        public void Update(Route entity)
        {
            this.routeRepository.Update(entity);
        }
        public List<Route> Find(List<Route> entity) { return null; }
        public List<Route> Sort(List<Route> entity) { return null; }

    }
}
