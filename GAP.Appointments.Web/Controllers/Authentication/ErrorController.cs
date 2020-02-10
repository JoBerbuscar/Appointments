using System.Web.Mvc;

namespace GAP.Appointments.Web.Controllers
{
    public class ErrorLoginController : Controller
    {
        // GET: Error
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}