using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using BikeMates.Domain.Entities;

namespace BikeMates.Contracts.Repositories
{
    public interface IRouteRepository : IRepository<Route, int>
    {
        Route Get(int id); //TODO: Move to the base IRepository interface
        IEnumerable<Route> GetAllRoutes();
        void SubscribeUser(Route route, User user);
        bool UnsubscribeUser(Route route, User user);
    }
}
