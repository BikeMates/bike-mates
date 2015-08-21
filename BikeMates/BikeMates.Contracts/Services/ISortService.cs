using BikeMates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Contracts.Services
{
 public interface ISortService //TODO: Remove this service. Move query and sort logic for route into the IRouteService
    {
        List<Route> Date(List<Route> entity);
        List<Route> NameRoute(List<Route> entity);
        List<Route> Participants(List<Route> entity);
    }
}
