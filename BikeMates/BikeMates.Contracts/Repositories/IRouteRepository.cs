using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using BikeMates.Domain.Entities;

namespace BikeMates.Contracts.Repositories
{
    public interface IRouteRepository : IRepository<Route>
    {
        Route Get(int id);
        List<Route> GetAllRoutes();
    }
}
