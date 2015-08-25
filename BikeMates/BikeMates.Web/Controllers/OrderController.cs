using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BikeMates.Web.Controllers
{
    public class OrderController : Controller //TODO: Remove this controller
    {
        // GET: Order
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}