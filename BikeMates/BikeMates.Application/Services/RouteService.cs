using System.Collections.Generic;
using BikeMates.Contracts.Repositories;
using BikeMates.Contracts.Services;
using BikeMates.Domain.Entities;

namespace BikeMates.Application.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository routeRepository;

        public RouteService(IRouteRepository routeRepository)
        {
            this.routeRepository = routeRepository;
        }

        public void Add(Route entity)
        {
            this.routeRepository.Add(entity);
        }

        public Route Find(int id)
        {
            return this.routeRepository.Find(id);
        }

        public void Update(Route entity)
        {
            this.routeRepository.Update(entity);
        }

        public void Delete(int id)
        {
            this.routeRepository.Delete(id);
        }

        public IEnumerable<Route> GetAll()
        {
            return this.routeRepository.GetAll();
        }

        public IEnumerable<Route> Search(RouteSearchParameters searchParameters)
        {
            return this.routeRepository.Search(searchParameters);
        }

        public void SubscribeUser(Route route, User user)		
        {
            this.routeRepository.SubscribeUser(route, user);
        }		
		 
        public bool UnsubscribeUser(Route route, User user)
        {
            return this.routeRepository.UnsubscribeUser(route, user);
        }

        public IEnumerable<Route> GetAllUserSubscribedRoutes(User user)
        {
            return this.routeRepository.GetAllSubscribedRoutesByUser(user);
        }

        public bool CheckIsUserSubscribedToRoute(Route route, User user)
        {
            return route.Subscribers.Contains(user);
        }
        
    }
}