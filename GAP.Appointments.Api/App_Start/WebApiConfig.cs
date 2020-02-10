using GAP.Appointments.Commo.ProveedoresDependencias;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GAP.Appointments.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Web API configuration and services
            //Se registra el encargado de resolver las instancias y dependencias
            IApplicationContext context = ContextRegistry.GetContext();
            config.DependencyResolver = new SolucionadorDependencia(context);

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


        }
    }
}
