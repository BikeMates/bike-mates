using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeMates.Domain.Entities;

namespace BikeMates.Contracts.Repositories
{
    public interface IRouteRepository : IRepository<Route, int>
    {
        IEnumerable<Route> Search(RouteSearchParameters searchParameters);
        //void SubscribeUser(Route route, User user);
        //bool UnsubscribeUser(Route route, User user);
    }
}
