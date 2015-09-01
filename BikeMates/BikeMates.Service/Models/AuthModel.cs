using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BikeMates.Service.Models
{
    public class AuthModel
    {
        public string token { get; set; }
        public string role { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public bool isAuthorized { get; set; }
    }
}