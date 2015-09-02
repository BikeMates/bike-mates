using System.Web.Mvc;

namespace BikeMates.Web.Controllers
{
    public class HomeController : Controller
    {
      
        public ActionResult Index()
        {            
            return View(); 
        } 
        
        public ActionResult RouteView()
        {
            ViewBag.Message = "Route View";

            return View();
        }
    }
}