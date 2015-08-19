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


public class HomeRouteListRepository
{
    private readonly BikeMatesDbContext context;

    public List<Route> GetAllRoutes()
    {
        List<Route> RouteViewList = new List<Route>();

        var allRoutes = context.Routes.ToList();

        foreach (var rot in allRoutes)
        {    
                 RouteViewList.Add(new Route()
                    {
                        Name=rot.Name,
                        MeetingPlace=rot.MeetingPlace,
                        Distance=rot.Distance,
                        Description=rot.Description,
                        Start=rot.Start,
                        ParticipantsCount=rot.ParticipantsCount          
                    });               
            }
        return RouteViewList;
    }
}
