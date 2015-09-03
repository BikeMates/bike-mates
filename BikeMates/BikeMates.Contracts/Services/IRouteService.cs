using System.Collections.Generic;
using BikeMates.Domain.Entities;

namespace BikeMates.Contracts.Services
{
    public interface IRouteService
    {
        void Add(Route route);
        Route Find(int routeId);
        void Update(Route route);
        void Delete(int routeId);
        IEnumerable<Route> GetAll();
        IEnumerable<Route> Search(RouteSearchParameters searchParameters);
        void SubscribeUser(int routeId, string userId);
        void UnsubscribeUser(int routeId, string userId);
        IEnumerable<Route> GetAllUserSubscribedRoutes(string userId);
        bool CheckIsUserSubscribedToRoute(int routeId, string userId);
        void UnbanRoutes(List<int> routeIds);
    }
}
