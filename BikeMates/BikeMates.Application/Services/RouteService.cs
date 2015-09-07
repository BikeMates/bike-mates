using System.Collections.Generic;
using System.Linq;
using BikeMates.Contracts.Data;
using BikeMates.Contracts.Repositories;
using BikeMates.Contracts.Services;
using BikeMates.Domain.Entities;

namespace BikeMates.Application.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository routeRepository;
        private readonly IUserRepository userRepository;

        public RouteService(IRouteRepository routeRepository, IUserRepository userRepository)
        {
            this.routeRepository = routeRepository;
            this.userRepository = userRepository;
        }

        public void Add(Route route)
        {
            this.routeRepository.Add(route);
        }

        public Route Find(int routeId)
        {
            return this.routeRepository.Find(routeId);
        }

        public void Update(Route route)
        {
            this.routeRepository.Update(route);
        }

        public void Delete(int routeId)
        {
            this.routeRepository.Delete(routeId);
        }

        public IEnumerable<Route> GetAll()
        {
            return this.routeRepository.GetAll();
        }

        public IEnumerable<RouteData> Search(RouteSearchParameters searchParameters)
        {
            return this.routeRepository.Search(searchParameters).ToList().Select(MapRoute);
        }

        public void SubscribeUser(int routeId, string userId)
        {
            User user = userRepository.Find(userId);
            Route route = routeRepository.Find(routeId);

            this.routeRepository.SubscribeUser(route, user);
        }

        public void UnsubscribeUser(int routeId, string userId)
        {
            User user = userRepository.Find(userId);
            Route route = routeRepository.Find(routeId);
            this.routeRepository.UnsubscribeUser(route, user);
        }

        public IEnumerable<Route> GetAllUserSubscribedRoutes(string userId)
        {
            User user = userRepository.Find(userId);
            return this.routeRepository.GetAllSubscribedRoutesByUser(user);
        }

        public bool CheckIsUserSubscribedToRoute(int routeId, string userId)
        {
            User user = userRepository.Find(userId);
            Route route = routeRepository.Find(routeId);
            return route.Subscribers.Contains(user);
        }


        public void UnbanRoutes(List<int> routeIds)
        {
            Route route;
            foreach (var id in routeIds)
            {
                route = Find(id);
                route.IsBanned = false;
                Update(route);
            }
        }


        public void BanRoute(int routeId)
        {
            var route = Find(routeId);
            route.IsBanned = true;
            Update(route);
        }

        private RouteData MapRoute(Route route)
        {
            return new RouteData
            {
                Id = route.Id,
                Title = route.Title,
                Description = route.Description,
                MeetingPlace = route.MeetingPlace,
                Start = route.Start,
                Distance = route.Distance,
                IsBanned = route.IsBanned,
                AuthorName = route.Author != null ? string.Format("{0} {1}", route.Author.FirstName, route.Author.SecondName) : string.Empty
            };
        }
    }
}