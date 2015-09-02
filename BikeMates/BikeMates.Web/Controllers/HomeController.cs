﻿using BikeMates.DataAccess;
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
      
        public ActionResult Index()
        {            
            return View(); 
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
        public ActionResult RouteView()
        {
            ViewBag.Message = "Route View";

            return View();
        }
    }
}