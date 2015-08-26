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
using BikeMates.Service.Controllers;

namespace BikeMates.Web.Controllers
{
    public class HomeController : Controller
    {
        DataAccess.Repository.RouteRepository rot = new DataAccess.Repository.RouteRepository(new BikeMatesDbContext());
        List<Route> r = new List<Route>();
        public ActionResult Index(int? page)
        {
           r=rot.GetAllRoutes().ToList();
            int pageNumber = page ?? 1;
            int pageSize = 3;
            return View(r.ToPagedList(pageNumber, pageSize)); //TODO: Remove SearchController from here. Use Knockout to search routes
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