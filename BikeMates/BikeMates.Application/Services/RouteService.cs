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
        public List<Route> Find(List<Route> entity,string Location,string Dist1,string Dist2,string Date1,string Date2) {
            //List<Route> results;
           
            //results=entity.FindAll(
            //    delegate (Route rot){
                   
            //        return String.Equals(rot.MeetingPlace, Location);
            //}
            //        );
            //results = entity.FindAll(
            //    delegate(Route rot)
            //    {

            //        return (rot.Distance >= Convert.ToInt32(Dist1) && rot.Distance <=Convert.ToInt32( Dist2));
            //    }
            //        );

            //results = entity.FindAll(
            //    delegate(Route rot)
            //    {

            //        return (rot.Start >=Convert.ToDateTime( Date1) && rot.Start <= Convert.ToDateTime(Date2));
            //    }
            //        );
            //if (Location == null || Dist1 == 0 || Dist2 == 0 || Date1 == new DateTime() || Date2 == new DateTime()) { return results; }
            return entity;
        }
        public List<Route> Sort(List<Route> entity, bool Date,bool Name,bool Participants) { 
            //    if (Name)
            //{

            //    entity.Sort(delegate(Route x, Route y) {
            //        return x.Name.CompareTo(y.Name);
            //    });
            //}

            //    if (Participants)
            //    {
            //        entity.Sort(delegate(Route x, Route y)
            //        {
            //            return x.ParticipantsCount.CompareTo(y.ParticipantsCount);
            //        });
            //    }
            //    if (Date)
            //    {
            //        entity.Sort(delegate(Route x, Route y)
            //        {
            //            return x.Start.CompareTo(y.Start);
            //        });
            //    }
            return entity; }

    }
}
