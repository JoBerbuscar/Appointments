using GAP.Appointments.Web.Models;
using System;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

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

        public ActionResult LogOut401()
        {
            HttpCookie cookie = new HttpCookie("Cookie1", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", null);
            //return RedirectToAction("Login", "Account", null);
        }
    }
}
