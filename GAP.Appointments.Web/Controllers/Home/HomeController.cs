using GAP.Appointments.Web.Models;
using System.Web.Mvc;

namespace GAP.Appointments.Web.Controllers.Home
{
    [CustomAuthorize(Roles = "User")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
