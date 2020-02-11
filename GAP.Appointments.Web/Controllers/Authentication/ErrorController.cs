using System.Web.Mvc;

namespace GAP.Appointments.Web.Controllers
{
    [AllowAnonymous]
    public class ErrorLoginController : Controller
    {
        // GET: Error
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}