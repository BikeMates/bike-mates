using System.Collections.Generic;
using BikeMates.Domain.Entities;

namespace BikeMates.Contracts.Services
{
    public interface IRouteService
    {
        void Add(Route route);
        Route Find(int id);
        void Update(Route route);
        void Delete(int id);
        IEnumerable<Route> GetAll();
        IEnumerable<Route> Search(RouteSearchParameters searchParameters);
        void SubscribeUser(Route route, User user);
        bool UnsubscribeUser(Route route, User user);
    }
}
