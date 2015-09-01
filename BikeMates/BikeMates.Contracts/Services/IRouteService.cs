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
        void Add(Route route);
        Route Get(int id);
        void Update(Route entity);
        void Delete(int id);
        IEnumerable<Route> GetAll();
        IEnumerable<Route> Search(RouteSearchParameters searchParameters);
    }
}
