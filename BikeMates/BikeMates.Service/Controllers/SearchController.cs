using BikeMates.Domain.Entities;
using BikeMates.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BikeMates.Service.Controllers
{
    public class SearchController : ApiController
    {

        // POST api/user
        public void Update( RouteSearch routelist)
        {
            routelist.Location = "wololo";
            routelist.Date1 = "wololo";
            routelist.Date2= "wololo";
            routelist.Distance1 = "wololo";
            routelist.Distance2 = "wololo";


            //userService.Update(user);
        }

        /*public string Get(RouteSearch routelist)
        {
           return routelist.Location;
        }
        */

    }
}
