using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeMates.Service.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName {get; set;}
        public string Picture {get; set;}
        public string About {get; set;}
        public bool IsBaned { get; set; }
    }
}