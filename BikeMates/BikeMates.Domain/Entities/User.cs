using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;

namespace BikeMates.Domain.Entities 
{
    public class User : IdentityUser
    {
        public User() { Id = Guid.NewGuid().ToString(); }
        public string FirstName { get; set; }
        public string SecondName {get; set;}
        public string Picture {get; set;}
        public string About {get; set;}

        public virtual ICollection<Route> Routes { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("FirstName", this.FirstName));
            userIdentity.AddClaim(new Claim("SecondName", this.SecondName));
            userIdentity.AddClaim(new Claim("Picture", this.Picture));
            userIdentity.AddClaim(new Claim("About", this.About));

            return userIdentity;
        }
    }
}
