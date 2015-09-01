using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using BikeMates.Contracts;
using BikeMates.Contracts.Repositories;
using BikeMates.Contracts.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BikeMates.Domain.Entities;

namespace BikeMates.DataAccess.Repository
{
    public class RouteRepository : IRepository<Route, int>
    {
        private readonly BikeMatesDbContext context;

        public RouteRepository(BikeMatesDbContext context)
        {
            this.context = context;
        }
        public Route Add(Route entity)
        {
            var route = this.context.Set<Route>().Add(entity); //TODO: Create property Routes for context.Set<Route>()
            this.context.SaveChanges();
            return route;
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            this.context.Set<Route>().Remove(entity);
            this.context.SaveChanges();
        }

        public IEnumerable<Route> GetAll()
        {
            return this.context.Routes;
        }

        public Route Get(int id)
        {
            return this.context.Set<Route>().Find(id);
        }

        public void Update(Route entity)
        {
            this.context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            this.context.SaveChanges();
        }

        public IEnumerable<Route> Search(RouteSearchParameters searchParameters)
        {
            var routes = context.Routes.Where(route =>
                (route.Title.Contains(searchParameters.Title) || string.IsNullOrEmpty(searchParameters.Title)) &&
                (route.Author.FirstName.Contains(searchParameters.Author) || string.IsNullOrEmpty(searchParameters.Author)) &&
                (route.Author.SecondName.Contains(searchParameters.Author) || string.IsNullOrEmpty(searchParameters.Author)) &&
                (route.MeetingPlace.Contains(searchParameters.MeetingPlace) || string.IsNullOrEmpty(searchParameters.MeetingPlace)) &&
                (route.Description.Contains(searchParameters.Description) || string.IsNullOrEmpty(searchParameters.Description)) &&
                (route.Distance >= searchParameters.MinDistance || searchParameters.MinDistance.HasValue) &&
                (route.Distance <= searchParameters.MaxDistance || searchParameters.MaxDistance.HasValue) &&
                (route.Start >= searchParameters.DateFrom  || searchParameters.DateFrom.HasValue) &&
                (route.Start <= searchParameters.DateTo || searchParameters.DateTo.HasValue)
                );
            if (searchParameters.SortOrder == SortBy.Date)
            {
                routes = routes.OrderBy(x => x.Start);
            }
            if (searchParameters.SortOrder == SortBy.Title)
            {
                routes = routes.OrderBy(x => x.Title);
            }
            if (searchParameters.SortOrder == SortBy.Subscribers)
            {
                routes = routes.OrderBy(x => x.Subscribers.Count);
            }
            return routes;;
        }
    }
}
