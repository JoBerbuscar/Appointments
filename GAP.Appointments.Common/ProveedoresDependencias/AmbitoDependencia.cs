#region Referencias 
using Spring.Context;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
#endregion

namespace GAP.Appointments.Commo.ProveedoresDependencias
{
    /// <summary>
    /// Scope personalizado para la implementación de la inyección de dependencia
    /// </summary>
    public class AmbitoDependencia : IDependencyScope
    {
        private IApplicationContext _context;

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="MP.EA.Common.Resolvers.EaDependencyScope"/>
        /// </summary>
        /// <param name="context"></param>
        internal AmbitoDependencia(IApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a service from the scope.
        /// </summary>
        /// <param name="serviceType">The service to be retrieved.</param>
        /// <returns>The retrieved service.</returns>
        public object GetService(Type serviceType)
        {
            return LocalizadorServicio.GetService(serviceType, _context, false);
        }

        /// <summary>
        /// Retrieves a collection of services from the scope.
        /// </summary>
        /// <param name="serviceType">The collection of services to be retrieved.</param>
        /// <returns>The retrieved collection of services.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            var services = new List<object>();
            var service = LocalizadorServicio.GetService(serviceType, _context, false);

            if (service != null)
                services.Add(service);

            return services;
        }

        /// <summary>
        /// Desecha el scope
        /// </summary>
        public void Dispose()
        {
            _context = null;
        }
    }
}
