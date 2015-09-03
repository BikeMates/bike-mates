using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using BikeMates.DataAccess.Managers;
using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BikeMates.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BikeMatesDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(BikeMatesDbContext context)
        {
            SeedBikeMates(context);
        }
        private void SeedBikeMates(BikeMatesDbContext context)
        {
            User user = new User();
            CreateRole("Admin", context);
            CreateRole("User", context);
            AddInitialAdmin("admin@admin.com", "admin", "admin", "Qwerty1#",context);

            int routesCount=0;

            for (int i = 0; i < 100; i++)
            {
                user = AddUser(i, context);

                for (int j = 0; j < 10; j++)
                {
                    routesCount++;
                    AddRoute(routesCount, user, context);
                }
            }
        }

        private void CreateRole(string roleName, BikeMatesDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var role = new IdentityRole();
            if (!roleManager.RoleExists(roleName))
            {
                role = new IdentityRole();
                role.Name = roleName;
                roleManager.Create(role);
            }
        }

        private void AddInitialAdmin(string email, string firstname, string lastname, string password, BikeMatesDbContext context)
        {
            var userManager = new UserManager(context);
            var admin = new User { Email = email, UserName = email, Role = "Admin", FirstName = firstname, SecondName = lastname };
            if (userManager.FindByEmail(email) == null)
            {
                userManager.Create(admin, password);
                userManager.AddToRole(admin.Id, "Admin");
            }
        }

        private void AddRoute(int index, User author, BikeMatesDbContext context)
        {
            Route route = new Route();
            Coordinate start = context.Coordinates.Add(new Coordinate
            {
                Latitude = 50.4653661,
                Longitude = 30.521903599999973
            });
            Coordinate end = context.Coordinates.Add(new Coordinate
            {
                Latitude = 50.466008599999988,
                Longitude = 30.511753699999986
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
                Title = "Test-route-" + index,
                IsBanned = false,
                MeetingPlace =
                    "Near the Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor.",
                MapData = mapdata,
                Author = author,
                Subscribers = subscribers
            });
            context.Routes.Add(route);
            context.SaveChanges();
        }

        private User AddUser(int index, BikeMatesDbContext context)
        {
            var userManager = new UserManager(context);
            User user = new User
            {
                FirstName = "Username-" + index,
                SecondName = "Username-" + index,
                Email = "Username-" + index + "@bikemates.com",
                EmailConfirmed = true,
                UserName = "Username-" + index + "@bikemates.com",
                About =
                    @"I'm just a test user. My whole life story:\nLorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. ",
                PasswordHash = "AEPu4Rln+Qak4i79daRuRlcAe70OQ+uvYDJreFYyoudb0iWaorIkAV/crymPqTpV6w==", //1234qwerQ_
                SecurityStamp = "b56fd8f5-0dac-4360-9def-175f9dab8aaf",
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            };

            user = context.Users.Add(user);
            context.SaveChanges();
            user.Role = "User";
            userManager.AddToRole(user.Id, "User");
            return user;
        }
    }
}
