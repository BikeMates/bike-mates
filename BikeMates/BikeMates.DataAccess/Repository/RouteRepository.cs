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

        public RouteRepository()
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
            return this.context.Routes.ToList();
        }

        public Route Get(int id)
        {
            return this.context.Set<Route>().Find(id);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public void Update(Route entity)
        {
            this.context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }
        public List<Route> GetAllRoutes()
        {
            return context.Routes.ToList();
        }
    }
}
