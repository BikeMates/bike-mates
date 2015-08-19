using BikeMates.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
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