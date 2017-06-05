using System.Web.Mvc;

namespace ServiceOrder.WebSite.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }
       
    }
}