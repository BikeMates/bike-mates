﻿using System;
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
    }
}
