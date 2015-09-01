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

            return routes;
        }
        //public void SubscribeRoute(Route route, User user)
        //{
        //    user.Subscriptions.Add(route);
        //    this.Update(user);
        //}
        //public bool UnsubscribeRoute(Route route, User user)
        //{
        //    bool isRemoved = user.Subscriptions.Remove(route);
        //    this.Update(user);
        //    return isRemoved;
        //}
    }
}
