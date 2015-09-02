using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BikeMates.Domain.Entities 
{
    public class User : IdentityUser, IEntity<string>
    {
        public User() { Id = Guid.NewGuid().ToString(); }
        public string FirstName { get; set; }
        public string SecondName {get; set;}
        public string Picture {get; set;}
        public string About {get; set;}
        public string Role { get; set; }
        public bool IsBanned { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
        public virtual ICollection<Route> Subscriptions { get; set; }
    }
}
