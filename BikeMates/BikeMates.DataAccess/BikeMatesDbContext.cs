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

        public static BikeMatesDbContext Create()
        {
            return new BikeMatesDbContext();
        }
    }
}
