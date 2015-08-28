using System.Collections.Generic;
using System.Collections.ObjectModel;
using BikeMates.Contracts.Repositories;
using BikeMates.DataAccess.Repository;
using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BikeMates.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BikeMates.DataAccess.BikeMatesDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(BikeMates.DataAccess.BikeMatesDbContext context)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();
            //SeedBikeMates(context);
        }
        private void SeedBikeMates(BikeMates.DataAccess.BikeMatesDbContext context)
        {
            BikeMates.Domain.Entities.User user = new BikeMates.Domain.Entities.User();
            BikeMates.Domain.Entities.Route route = new BikeMates.Domain.Entities.Route();

            for (int i = 0; i < 100; i++)
            {
                user = context.Users.Add(new User
                {
                    FirstName = "Username-" + i,
                    SecondName = "Username-" + i,
                    Email = "Username-" + i + "@bikemates.com",
                    EmailConfirmed = true,
                    UserName = "Username-" + i + "@bikemates.com",
                    About = "I'm just a test user. My whole life story:\n" +
                            "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor." +
                            " Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus." +
                            " Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. ",
                    PasswordHash = "AEPu4Rln+Qak4i79daRuRlcAe70OQ+uvYDJreFYyoudb0iWaorIkAV/crymPqTpV6w==", //1234qwerQ_
                    SecurityStamp = "b56fd8f5-0dac-4360-9def-175f9dab8aaf",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                });
                for (int j = 0; j < 10; j++)
                {
                    Coordinate start = context.Coordinates.Add(new Coordinate
                    {
                        Latitude = 50.4602253,
                        Longitude = 30.511327100000017
                    });
                    Coordinate end = context.Coordinates.Add(new Coordinate
                    {
                        Latitude = 50.4602253,
                        Longitude = 30.511327100000017
                    });
                    MapData mapdata = context.MapDatas.Add(new MapData { Start = start, End = end });

                    Collection<User> subscribers = new Collection<User>();
                    var randomizer = new Random();

                    if (context.Users.Count() > 10)
                    {
                        for (int k = 0; k < 5; k++)
                        {
                            subscribers.Add(context.Users.ElementAt(randomizer.Next(context.Users.Count())));
                        }
                    }
                    route = context.Routes.Add(new Route
                    {
                        Start = DateTime.Today,
                        Distance = 1.897,
                        Description =
                            "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor." +
                            " Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus." +
                            " Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. ",
                        Title = "Test-route-" + i,
                        IsBanned = false,
                        MeetingPlace =
                            "Near the Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor.",
                        MapData = mapdata,
                        Author = user,
                        //Subscribers = subscribers
                    });
                    user.Routes.Add(route);
                    context.Users.AddOrUpdate(context.Users.Find(user));
                }
            }
        }
    }
}
