using BikeMates.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace BikeMates.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly BikeMatesDbContext context;
        public ActionResult Index()
        {
            HomeRouteListRepository routelist = new HomeRouteListRepository();
            var allRoutes = routelist.GetAllRoutes().ToList();

            return View(allRoutes);
            return View();
        }

        public string GetInfo()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = HttpContext.User.Identity.Name;
            var firstName = identity.Claims.Where(c => c.Type == "FirstName").Select(c => c.Value).SingleOrDefault();
            var secondName = identity.Claims.Where(c => c.Type == "SecondName").Select(c => c.Value).SingleOrDefault();
            var picture = identity.Claims.Where(c => c.Type == "Picture").Select(c => c.Value).SingleOrDefault();
            var about = identity.Claims.Where(c => c.Type == "About").Select(c => c.Value).SingleOrDefault();
            return "<p>Email: " + email + "</p><p>FirstName:" + firstName + "</p><p> SecondName:" + secondName + "</p><p> Picture:" + picture + "</p><p> About:" + about + "</p>";
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}