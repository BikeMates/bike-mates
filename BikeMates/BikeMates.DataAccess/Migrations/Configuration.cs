using System.Collections.Generic;
using System.Collections.ObjectModel;
using BikeMates.Contracts.Repositories;
using BikeMates.DataAccess.Managers;
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
            SeedBikeMates(context);
        }
        private void SeedBikeMates(BikeMates.DataAccess.BikeMatesDbContext context)
        {
            User user = new User();
            Route route = new Route();
            var userManager = new UserManager(context);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role = new IdentityRole();
            if (!roleManager.RoleExists("Admin"))
            {
                role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("User"))
            {
                role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }

            var admin = new User { Email = "admin@admin.com", UserName = "admin@admin.com", Role = "Admin" };
            string password = "Qwerty1#";
            userManager.Create(admin, password);

            userManager.AddToRole(admin.Id, "Admin");


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
                context.Users.Add(user);
                context.SaveChanges();
                user.Role = "User";
                userManager.AddToRole(user.Id, "User");

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

                    if (context.Users.Count() > 10)
                    {
                        var randomUsers = context.Users.OrderBy(r => Guid.NewGuid()).Take(5);
                        foreach (var item in randomUsers)
                        {
                            subscribers.Add(item);
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
                        Title = "Test-route-" + j,
                        IsBanned = false,
                        MeetingPlace =
                            "Near the Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor.",
                        MapData = mapdata,
                        Author = user,
                        Subscribers = subscribers
                    });
                    context.Routes.Add(route);
                    context.SaveChanges();
                }
            }
        }
    }
}
