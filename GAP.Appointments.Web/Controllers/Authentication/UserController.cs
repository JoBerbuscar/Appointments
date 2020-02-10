using System.Web.Mvc;
using GAP.Appointments.Web.Models;

namespace GAP.Appointments.Web.Controllers
{
    [CustomAuthorize(Roles = "User")]
    public class UserController : Controller
    {
        
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}