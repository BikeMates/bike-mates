using BikeMates.Contracts;
using BikeMates.Contracts.Repositories;
using BikeMates.Contracts.Services;
using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeMates.DataAccess.Repository;

namespace BikeMates.Application.Services
{
    public class RouteService : IRouteService
    {
        private RouteRepository routeRepository;

        public RouteService(RouteRepository routeRepository)
        {
            this.routeRepository = routeRepository;
        }
        public void Add(Route entity)
        {
            this.routeRepository.Add(entity);
        }
        public Route Get(int id)
        {
            return this.routeRepository.Get(id);
        }
        public void Update(Route entity)
        {
            this.routeRepository.Update(entity);
        }
        public void Delete(int id)
        {
            routeRepository.Delete(id);
        }
        public IEnumerable<Route> GetAll()
        {
            return routeRepository.GetAll();
        }
        public IEnumerable<Route> Search(RouteSearchParameters searchParameters)
        {
            return routeRepository.Search(searchParameters);
        }

        public void SubscribeUser(Route route, User user)		
        {		
            route.Subscribers.Add(user);		
            this.Update(route);		
        }		
		 
        public bool UnsubscribeUser(Route route, User user)
        {		
            bool isRemoved = route.Subscribers.Remove(user);		
            this.Update(route);		
            return isRemoved;		
        }
    }
}