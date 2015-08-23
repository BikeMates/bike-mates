using BikeMates.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using PagedList;
using BikeMates.Domain.Entities;

namespace BikeMates.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly BikeMatesDbContext context;
        private DataAccess.Repository.HomeRouteListRepository routelist = new DataAccess.Repository.HomeRouteListRepository(); 
        private List<Route> allRoutes;
        public ActionResult Index(int? page)
        {
            allRoutes = routelist.GetAllRoutes().ToList(); //TODO: Move this logic into WebAPI Service and take data from it using Knockout.
            
            int pageNumber = page ?? 1;
            int pageSize = 3;
            return View(allRoutes.ToPagedList(pageNumber,pageSize));
        }
        [Authorize]
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