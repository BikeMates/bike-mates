using System.Collections.Generic;
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
            IQueryable<Route> routes = this.Context.Routes.Where(route =>
                (string.IsNullOrEmpty(searchParameters.Title) || route.Title.Contains(searchParameters.Title)) &&
                (string.IsNullOrEmpty(searchParameters.Author) || route.Author.FirstName.Contains(searchParameters.Author)) &&
                (string.IsNullOrEmpty(searchParameters.Author)) || route.Author.SecondName.Contains(searchParameters.Author) &&
                (string.IsNullOrEmpty(searchParameters.MeetingPlace) || route.MeetingPlace.Contains(searchParameters.MeetingPlace)) &&
                (string.IsNullOrEmpty(searchParameters.Description) || route.Description.Contains(searchParameters.Description)) &&
                (!searchParameters.MinDistance.HasValue || route.Distance >= searchParameters.MinDistance) &&
                (!searchParameters.MaxDistance.HasValue || route.Distance <= searchParameters.MaxDistance) &&
                (!searchParameters.DateFrom.HasValue || route.Start >= searchParameters.DateFrom) &&
                (!searchParameters.DateTo.HasValue || route.Start <= searchParameters.DateTo)
                );

            if (searchParameters.SortOrder == RouteSortBy.Date)
            {
                routes = routes.OrderBy(x => x.Start);
            }

            if (searchParameters.SortOrder == RouteSortBy.Title)
            {
                routes = routes.OrderBy(x => x.Title);
            }

            if (searchParameters.SortOrder == RouteSortBy.Subscribers)
            {
                routes = routes.OrderBy(x => x.Subscribers.Count);
            }

            return routes;
        }
        public void SubscribeUser(Route route, User user)
        {
            route.Subscribers.Add(user);
            this.Update(route);
        }
        public bool UnsubscribeUser(Route route, User user)
        {
            bool isRemoved = route.Subscribers.Remove(user);
            this.Update(route);
            return isRemoved;
        }
    }
}
