using BikeMates.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Contracts.Services
{
   public interface ISearchService
    {
       List<Route> Distance(List<Route> entity, string dis_start, string dis_end);
       List<Route> Location(List<Route> entity, string NameLocal);
       List<Route> Date(List<Route> entity, string dis_start, string dis_end);
       List<Route> NameRoute(List<Route> entity, string NameRoute);
       List<Route> Participants(List<Route> entity, string participants);
       
       
    }
}
