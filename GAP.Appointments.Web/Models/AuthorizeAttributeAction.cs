﻿using System.Web;
using System.Web.Mvc;

namespace GAP.Appointments.Web.Models
{
    public class AuthorizeAttributeAction : AuthorizeAttribute
    {
        protected virtual CustomPrincipal CurrentUser
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return ((CurrentUser != null && !CurrentUser.IsInRole(Roles)) || CurrentUser == null) ? false : true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // If they are authorized, handle accordingly
            if (this.AuthorizeCore(filterContext.HttpContext))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                RedirectToRouteResult routeData = null;
                routeData = new RedirectToRouteResult
                    (new System.Web.Routing.RouteValueDictionary
                     (new
                     {
                         controller = "ErroLogin",
                         action = "AccessDenied"
                     }
                     ));
                // Otherwise redirect to your specific authorized area
                filterContext.Result = routeData;
            }
        }
    }
}