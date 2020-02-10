#region Referencias
using System;
using Spring.Context;
#endregion

namespace GAP.Appointments.Commo.ProveedoresDependencias
{
    public class SolucionadorDependencia : AmbitoDependencia, System.Web.Http.Dependencies.IDependencyResolver,
        System.Web.Mvc.IDependencyResolver
    {
        private IApplicationContext _context;

        /// <summary>
        /// Crea una nueva instancia del tipo <see cref="MasGlobal.Employees.Common.ProveedoresDependencias.SolucionadorDependencia"/>
        /// </summary>
        /// <param name="context"></param>
        public SolucionadorDependencia(IApplicationContext context) : base(context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// Starts a resolution scope
        /// </summary>
        /// <returns>The dependency scope.</returns>
        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            return new AmbitoDependencia(_context);
        }
    }
}
