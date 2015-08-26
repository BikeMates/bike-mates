using BikeMates.Contracts;
using BikeMates.Contracts.Repositories;
using BikeMates.Contracts.Services;
using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Application.Services
{
    public class RouteService : IRouteService
    {
        private IRouteRepository routeRepository;

        public RouteService(IRouteRepository routeRepository)
        {
            this.routeRepository = routeRepository;
        }
        public void Add(Route entity)
        {
            this.routeRepository.Add(entity);
        }
        public Route GetRoute(int id)
        {
            return this.routeRepository.Get(id);
        }
        public void Delete(int id)
        {
            routeRepository.Delete(id);
        }

        public void Update(Route entity)
        {
            this.routeRepository.Update(entity);
        }

        //TODO: Replace Find and Sort methods with one method SearchRoutes(RoutesSearchParameters routesSearchParameters), where RoutesSearchParameters contains all needed search and sort fields.
        //TODO: Use LINQ query to get all routes and sort them. 
        public List<Route> Find(RoutesSearchParameters routesSearchParameters)
        {
            List<Route> results;

            results = routesSearchParameters.entity.FindAll(
                delegate(Route rot)
                {

                    return String.Equals(rot.MeetingPlace, routesSearchParameters.Location);
                }
                    );
            results = routesSearchParameters.entity.FindAll(
                delegate(Route rot)
                {

                    return (rot.Distance >= Convert.ToInt32(routesSearchParameters.Dist1) && rot.Distance <= Convert.ToInt32(routesSearchParameters.Dist2));
                }
                    );

            results = routesSearchParameters.entity.FindAll(
                delegate(Route rot)
                {

                    return (rot.Start >= Convert.ToDateTime(routesSearchParameters.Date1) && rot.Start <= Convert.ToDateTime(routesSearchParameters.Date2));
                }
                    );

            if (routesSearchParameters.Name)
            {

                routesSearchParameters.entity.Sort(delegate(Route x, Route y)
                {
                    return x.Title.CompareTo(y.Title);
                });
            }

           /* if (routesSearchParameters.Participants)
            {
                routesSearchParameters.entity.Sort(delegate(Route x, Route y)
                {
                    return x.Participants.CompareTo(y.Participants);
                });
            }*/
            if (routesSearchParameters.Date)
            {
                routesSearchParameters.entity.Sort(delegate(Route x, Route y)
                {
                    return x.Start.CompareTo(y.Start);
                });
            }
            return routesSearchParameters.entity;
        }



    }
}