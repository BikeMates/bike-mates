using System.Collections.Generic;
using BikeMates.Domain.Entities;

namespace BikeMates.Contracts.Repositories
{
    public interface IRouteRepository : IRepository<Route, int>
    {
        IEnumerable<Route> Search(RouteSearchParameters searchParameters);
        void SubscribeUser(Route route, User user);
        void UnsubscribeUser(Route route, User user); 
        IEnumerable<Route> GetAllSubscribedRoutesByUser(User user);
    }
}
