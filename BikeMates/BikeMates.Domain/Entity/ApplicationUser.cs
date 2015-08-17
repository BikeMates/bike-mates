using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;

namespace BikeMates.DataAccess.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { Id = Guid.NewGuid().ToString(); }
        public string FirstName { get; set; }
        public string SecondName {get; set;}
        public string Picture {get; set;}
        public string About {get; set;}

        public virtual ICollection<Route> Routes { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
