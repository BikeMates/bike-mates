using BikeMates.Domain.Entities;
using BikeMates.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Application.Services
{
   
  public  class SearchHome: ISearchService
    {
      private List<Route> allroute = new List<Route>();
        public List<Route> Distance(List<Route> entity, string dis_start,string dis_end)
        {
            if (dis_start != null && dis_end != null) {
                foreach (Route rot in allroute) { 
                if(rot.Distance < Convert.ToInt32(dis_start) && rot.Distance > Convert.ToInt32(dis_end)){
                    allroute.Remove(rot);
                }
                }
            }
            return allroute;
        }

        public List<Route> Location(List<Route> entity, string NameLocal)
        {
            throw new NotImplementedException();
        }

        public List<Route> Date(List<Route> entity)
        {
            throw new NotImplementedException();
        }

        public List<Route> NameRoute(List<Route> entity)
        {
            throw new NotImplementedException();
        }
    }
}
