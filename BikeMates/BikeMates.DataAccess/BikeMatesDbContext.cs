using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.DataAccess
{
    public class BikeMatesDbContext : IdentityDbContext<User>
    {
        public BikeMatesDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Route> Routes { get; set; }
        public DbSet<Coordinate> Coordinates { get; set; }
        public DbSet<MapData> MapDatas { get; set; }
       
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new {r.RoleId, r.UserId});

            modelBuilder.Entity<User>()
                .HasMany(x => x.Subscriptions)
                .WithMany(r => r.Subscribers).
                Map(m =>
                {
                    m.ToTable("Subscriptions");
                    m.MapLeftKey("User_Id");
                    m.MapRightKey("Route_Id");
                }
                );

        }

        
        public static BikeMatesDbContext Create()
        {
            return new BikeMatesDbContext();
        }
    }
}
