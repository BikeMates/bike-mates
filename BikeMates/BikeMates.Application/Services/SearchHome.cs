using BikeMates.Domain.Entities;
using BikeMates.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//uder usings
using BikeMates.DataAccess.Repository;

namespace BikeMates.Application.Services
{
   
  public  class SearchHome: ISearchService
    {
      private DataAccess.Repository.HomeRouteListRepository routelist = new DataAccess.Repository.HomeRouteListRepository();
      private List<Route> allRoutes;
      public List<Route> get(){
          allRoutes=routelist.GetAllRoutes();
          return allRoutes;
      }
    
        public List<Route> Distance( string dis_start,string dis_end)
        {
            List<Route> list_distance = new List<Route>();
            foreach (Route rot in get())
            {
                if (rot.Distance >= Convert.ToInt32(dis_start) && rot.Distance <= Convert.ToInt32(dis_end)) {
                    list_distance.Add(rot);
                }
            }
            return list_distance;
        }

        public List<Route> Location(string NameLocal)
        {
            
            List<Route> list_local = new List<Route>();
            foreach (Route rot in get())
            {
                if (rot.MeetingPlace.Contains(NameLocal))
                {
                    list_local.Add(rot);
                }
            }
            return list_local;
        }

        public List<Route> Date(string dis_start, string dis_end)
        {
            List<Route> list_Date = new List<Route>();
            foreach (Route rot in get())
            {
                if (rot.Start >= Convert.ToDateTime(dis_start) && rot.Start <= Convert.ToDateTime(dis_end))
                {
                    list_Date.Add(rot);
                }
            }
            return list_Date;
        }


        public List<Route> NameRoute( string NameRoute)
        {
            List<Route> list_name = new List<Route>();
            foreach (Route rot in get())
            {
                if (rot.Name.Contains(NameRoute))
                {
                    list_name.Add(rot);
                }
            }
            return list_name;
        }

        public List<Route> Participants( string participants)
        {
            throw new NotImplementedException();
        }
    }
}
