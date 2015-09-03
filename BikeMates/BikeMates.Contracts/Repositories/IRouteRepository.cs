using System.Collections.Generic;
using BikeMates.Domain.Entities;

namespace BikeMates.Contracts.Repositories
{
    public interface IRouteRepository : IRepository<Route, int>
    {
        IEnumerable<Route> Search(RouteSearchParameters searchParameters);
        void SubscribeUser(Route route, User user);
        bool UnsubscribeUser(Route route, User user); //TODO: Remove bool result. Make the method void
        IEnumerable<Route> GetAllSubscribedRoutesByUser(User user);
    }
}
