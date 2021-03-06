﻿using System.Collections.Generic;
using System.Linq;
using BikeMates.Contracts.Repositories;
using BikeMates.Domain.Entities;

namespace BikeMates.DataAccess.Repository
{
    public class RouteRepository : Repository<Route, int>, IRouteRepository
    {
        public RouteRepository(BikeMatesDbContext context)
            : base(context)
        {   }

        public IEnumerable<Route> Search(RouteSearchParameters searchParameters)
        {
            IQueryable<Route> routes = this.Context.Routes.Where(route => !route.IsBanned
                && (string.IsNullOrEmpty(searchParameters.MeetingPlace) || route.MeetingPlace.Contains(searchParameters.MeetingPlace) || route.Description.Contains(searchParameters.MeetingPlace))
                &&
                (!searchParameters.MinDistance.HasValue || route.Distance >= searchParameters.MinDistance) &&
                (!searchParameters.MaxDistance.HasValue || route.Distance <= searchParameters.MaxDistance) &&
                (!searchParameters.DateFrom.HasValue || route.Start >= searchParameters.DateFrom) &&
                (!searchParameters.DateTo.HasValue || route.Start <= searchParameters.DateTo)
                );
            
            if (searchParameters.SortOrder == RouteSortOptions.Date)
            {
                routes = routes.OrderBy(x => x.Start);

            }

            if (searchParameters.SortOrder == RouteSortOptions.Title)
            {
                routes = routes.OrderBy(x => x.Title);
            }

            if (searchParameters.SortOrder == RouteSortOptions.Subscribers)
            {
                routes = routes.OrderBy(x => x.Subscribers.Count);
            }

            if (searchParameters.SortOrder == RouteSortOptions.MyRoutes)
            {
                routes = routes.Where(route => route.Author.Id.Equals(searchParameters.AuthorId));
            }
            if (searchParameters.SortOrder == RouteSortOptions.Subscribers)
            {
                routes = routes.Where(route => route.Author.Id.Equals(searchParameters.AuthorId));
            }

            if (searchParameters.AuthorId != null)
            {
                if (searchParameters.SearchType.Equals("MyRoutes"))
                {
                    routes = routes.Where(r => r.Author.Id == searchParameters.AuthorId);
                }

                if (searchParameters.SearchType.Equals("Subscription"))
                {
                    routes = routes.Where(r => r.Subscribers.Any(u => u.Id == searchParameters.AuthorId));
                }
            }

            //routes.Skip(searchParameters.PageNumber * searchParameters.PageSize).Take(searchParameters.PageSize);

            //TODO: Add paging here. Take only needed routes
            return routes.Skip(searchParameters.PageNumber * searchParameters.PageSize).Take(searchParameters.PageSize); 
        }

        public void SubscribeUser(Route route, User user)
        {
            route.Subscribers.Add(user);
            this.Update(route);
        }

        public void UnsubscribeUser(Route route, User user)
        {
            route.Subscribers.Remove(user); 
            this.Update(route);
        }

        public IEnumerable<Route> GetAllSubscribedRoutesByUser(User user)
        {
            IQueryable<Route> routes = this.Context.Routes.Where(route => route.Subscribers.Contains(user));
            return routes;
        }

       
    }
}
