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
       List<Route> Distance(string dis_start, string dis_end);
       List<Route> Location( string NameLocal);
       List<Route> Date(string dis_start, string dis_end);
       List<Route> NameRoute(string NameRoute);
       List<Route> Participants(string participants);
       
       
    }
}
