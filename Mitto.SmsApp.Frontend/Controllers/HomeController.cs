using System.Web.Mvc;

namespace Mitto.SmsApp.Frontend.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Statistics()
        {
            return View();
        }
        public ActionResult CountryList()
        {
            return View();
        }
    }
}