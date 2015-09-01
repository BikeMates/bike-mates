using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Contracts.Services
{
    public interface IRouteService
    {
        Route Add(Route route);
        Route GetRoute(int id);
        void Delete(int id);
        void Update(Route entity);//TODO: Rename entity to route. 
        List<Route> Find(RoutesSearchParameters routesSearchParameters);
      

    }
}
