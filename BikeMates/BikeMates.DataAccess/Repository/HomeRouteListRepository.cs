using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeMates.Contracts;
using BikeMates.Contracts.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BikeMates.Domain.Entities;
using BikeMates.DataAccess;

namespace BikeMates.DataAccess.Repository
{
    public class HomeRouteListRepository
    {
        private readonly BikeMatesDbContext context;

        public List<Route> GetAllRoutes()
        {
            List<Route> RouteViewList = new List<Route>();

            //List<Route> allRoutes = context.Routes.ToList();

            for (int rot = 0; rot <= 10; rot++)
            {
                RouteViewList.Add(new Route()
                   {
                       /* Name=rot.Name,
                        MeetingPlace=rot.MeetingPlace,
                        Distance=rot.Distance,
                        Description=rot.Description,
                        Start=rot.Start,
                        ParticipantsCount=rot.ParticipantsCount*/
                       Name = "Route Name",
                       MeetingPlace = "Lviv st. Patona",
                       Distance = 20,
                       Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                       Start = new DateTime(26 / 07 / 2015),
                       ParticipantsCount = 5
                   });
            }
            return RouteViewList;
        }
    }
}
