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

namespace BikeMates.DataAccess.Repository
{
    public class RouteRepository : IRouteRepository //TODO: Create a base Repository class and move common logic into it
    {
        private readonly BikeMatesDbContext context;

        public RouteRepository(BikeMatesDbContext context)
        {
            this.context = context;
        }

        public RouteRepository() //TODO: Remove this constructor. Do not create instances inside constructor
        {
            this.context = new BikeMatesDbContext();
        }
        public void Add(Route entity)
        {

            this.context.Set<Route>().Add(entity);
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            this.context.Set<Route>().Remove(entity);
            SaveChanges();
        }

        public IEnumerable<Route> GetAll()
        {
            return this.context.Routes.ToList(); //TODO: Remove ToList()
        }

        public Route Get(int id)
        {
            return this.context.Set<Route>().Find(id);
        }

        public void SaveChanges() //TODO: Remove this method
        {
            this.context.SaveChanges();
        }

        public void Update(Route entity)
        {
            this.context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }
        public List<Route> GetAllRoutes()
        {
           /* List<Route> rot = new List<Route>();
            for (int i = 0; i < 10; i++)
            {
                rot.Add(new Route()
                {
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ",
                    Distance = 20,
                    IsBanned = false,
                    Id = 3425423,
                    MeetingPlace = "st. Patona",
                    Participants = null,
                    Start = new DateTime(23 / 04 / 2015),
                    Title = "Greed"

                });
            }
               return rot.ToList();*/
            return context.Routes.ToList();
        }
    }
}
