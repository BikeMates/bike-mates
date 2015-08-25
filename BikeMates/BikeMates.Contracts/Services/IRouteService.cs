﻿using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Contracts.Services
{
    public interface IRouteService
    {
        //TODO: Rename entity to route. 
        void Add(Route entity);
        Route GetRoute(int id);
        void Delete(int id);
        void Update(Route entity);
        List<Route> Find(List<Route> entity, string Location, string Dist1, string Dist2, string Date1, string Date2);
        List<Route> Sort(List<Route> entity, bool Date, bool Name, bool Participants);

    }
}
