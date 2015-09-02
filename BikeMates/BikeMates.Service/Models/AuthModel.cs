using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeMates.Service.Models
{
    public class AuthModel
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public bool IsAuthorized { get; set; }
    }
}