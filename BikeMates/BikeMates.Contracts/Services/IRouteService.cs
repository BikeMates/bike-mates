using System.Collections.Generic;
using BikeMates.Domain.Entities;

namespace BikeMates.Contracts.Services
{
    public interface IRouteService
    {
        //TODO: Rename id to routeId. Be more specific
        void Add(Route route);
        Route Find(int id);
        void Update(Route route);
        void Delete(int id);
        IEnumerable<Route> GetAll();
        IEnumerable<Route> Search(RouteSearchParameters searchParameters);
        void SubscribeUser(int routeId, string userId);
        void UnsubscribeUser(int routeId, string userId);
        IEnumerable<Route> GetAllUserSubscribedRoutes(string userId);
        bool CheckIsUserSubscribedToRoute(int routeId, string userId);
    }
}
